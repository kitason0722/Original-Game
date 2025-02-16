using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControl : MonoBehaviour
{
    public int hp = 10;//プレイヤーのHP
    public float rotatespeed = 2.5f;//回転速度
    public float movespeed = 1.5f;//移動速度
    public float maxspped = 6.0f;//最高移動速度
    public float interval = 0.0f;//弾を打つまでのインターバルのカウント変数
    public float shotinterval = 0.5f;//弾を打つまでのインターバル
    public bool isPlayer = true;//プレイヤーかどうか
    public bool isRuby = true;//チームの判別
    public Vector3 dir;//プレイヤーが向いている方向
    private float _deathinterval = 0.0f;//死亡時のインターバルのカウント変数
    private float deathinterval = 3.0f;//死亡時のインターバル
    private float _flashtime = 0.0f;//点滅時間のカウント変数
    private float flashtime = 2.0f;//点滅時間
    private float cycle = 0.5f;//点滅の周期
    [SerializeField] private Renderer flash;//点滅用の変数

    private BulletPool bulletPool;
    public GameObject bulletposition;
    public Rigidbody2D rigid2D;
    private PlayerSpawn_Ruby playerSpawn_Ruby;
    private PlayerSpawn_Sapphire playerSpawn_Sapphire;
    public StateMachine stateMachine;

    [SerializeField] private GameObject arrowprefab;
    private List<GameObject> arrows = new List<GameObject>();
    private float arrowrad = 2.0f;

    private AudioSource audioSource;
    public AudioClip shotse;
    public AudioClip damagese1;
    public AudioClip damagese2;

    private enum State//プレイヤーの状態
    {
        Active,
        Idle,
        Dead
    }

    private State state = State.Active;

    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
        stateMachine.TransitionTo(new MoveState(this,false));
        audioSource = GetComponent<AudioSource>();
        playerSpawn_Ruby = FindObjectOfType<PlayerSpawn_Ruby>();
        playerSpawn_Sapphire = FindObjectOfType<PlayerSpawn_Sapphire>();
    }

    private void Start()
    {
        // 動的にSapphireチームのプレイヤーを取得
        List<PlayerControl> enemies = new List<PlayerControl>();
        enemies.AddRange(PlayerSpawn_Sapphire.GetPlayers());
        for (int i = 0; i < enemies.Count; i++)
        {
            GameObject arrow = Instantiate(arrowprefab, transform);
            arrow.SetActive(false);
            arrows.Add(arrow);
        }
    }

    private void Update()
    {
        dir = transform.up.normalized;//プレイヤーの向いている方向を常に取得
        interval += Time.deltaTime;//時間の計測

        //プレイヤーの処理
        if(isPlayer)
        {
            //HPゲージ確認用
            //if (Input.GetKeyDown(KeyCode.O)) if (hp > 0) hp--;
            //if (Input.GetKeyDown(KeyCode.P)) if (hp < 10) hp++;

            ArrowShow();

            switch (state)
            {
                case State.Active:
                Move();
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    if(interval > shotinterval)
                    {
                        Shot();
                        interval = 0.0f;
                    }
                }
                if(hp==0)state = State.Dead;
                break;

                case State.Dead:
                _deathinterval += Time.deltaTime;
                if(_deathinterval > deathinterval)
                {
                    _deathinterval = 0.0f;
                    if (isRuby) playerSpawn_Ruby.RespawnPlayer(this);
                    else playerSpawn_Sapphire.RespawnPlayer(this);
                    state = State.Idle;
                }
                break;

                case State.Idle:
                _flashtime += Time.deltaTime;
                if(_flashtime <= flashtime)
                {
                    var repeatValue = Mathf.Repeat(_flashtime,cycle);
                    flash.enabled = repeatValue >= cycle * 0.5f;
                }
                else
                {
                    _flashtime = 0.0f;
                    flash.enabled = true;
                    hp = 10;
                    state = State.Active;
                }
                break;
            }
        }

        //COMの処理
        else
        {
            switch(state)
            {
                case State.Active:
                stateMachine.Update();
                break;

                case State.Dead:
                _deathinterval += Time.deltaTime;
                if(_deathinterval > deathinterval)
                {
                    _deathinterval = 0.0f;
                    if(isRuby)playerSpawn_Ruby.RespawnPlayer(this);
                    else playerSpawn_Sapphire.RespawnPlayer(this);
                    state = State.Idle;
                }
                break;

                case State.Idle:
                _flashtime += Time.deltaTime;
                if(_flashtime <= flashtime)
                {
                    var repeatValue = Mathf.Repeat(_flashtime,cycle);
                    flash.enabled = repeatValue >= cycle * 0.5f;
                }
                else
                {
                    _flashtime = 0.0f;
                    flash.enabled = true;
                    state = State.Active;
                }
                break;
            }
        }
    }

    //移動処理
    private void Move()
    {
        if(Input.GetKey(KeyCode.W))
        {
            rigid2D.AddForce(transform.up * movespeed);
            if(rigid2D.velocity.magnitude > maxspped)
            {
                rigid2D.velocity = rigid2D.velocity.normalized * maxspped; 
            }
        }
        if(Input.GetKey(KeyCode.S))
        {
            rigid2D.velocity *= 0.985f;
            if(rigid2D.velocity.magnitude < 0.01f)
            {
                rigid2D.velocity = Vector2.zero;//完全停止
            }
        }
        if(Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0,0,rotatespeed);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0,0,-rotatespeed);
        }
    }

    //弾の撃ち出し
    public void Shot()
    {
        bulletPool.GetBullet(bulletposition.transform.position,transform.rotation,isRuby);
        audioSource.PlayOneShot(shotse);
    }

    //プールのセット
    public void SetBulletPool(BulletPool pool)
    {
        bulletPool = pool;
    }

    // 矢印の表示
    private void ArrowShow()
    {
        // 動的にSapphireチームのプレイヤーを取得
        List<PlayerControl> enemies = new List<PlayerControl>();
        enemies.AddRange(PlayerSpawn_Sapphire.GetPlayers());

        for (int i = 0; i < enemies.Count; i++)
        {
            PlayerControl enemy = enemies[i];
            if (enemy == null) continue;

            Vector3 dir = (enemy.transform.position - transform.position).normalized;

            Vector3 arrowpos = transform.position + dir * arrowrad;
            arrows[i].transform.position = new Vector3(arrowpos.x, arrowpos.y, 0);

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            arrows[i].transform.rotation = Quaternion.Euler(0, 0, angle - 90);
            arrows[i].SetActive(true);
        }
    }
    
    //衝突時の処理
    protected void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.CompareTag("Bullet"))
        {
            //弾のチーム判別を行う
            Bullet bullet = obj.gameObject.GetComponent<Bullet>();

            if(bullet == null)
            {
                Debug.LogError("Bulletコンポーネントが見つかりません。");
            }

            if ((isRuby == true && bullet.isRuby_bullet == false) || (isRuby == false && bullet.isRuby_bullet == true))
            {
                hp -= 1;
                if(0 < hp && hp <10)
                {
                    audioSource.PlayOneShot(damagese1);
                }
                if (hp <= 0)
                {
                    audioSource.PlayOneShot(damagese2);
                    hp = 0;
                    state = State.Dead;
                }
            }
        }

        if(obj.gameObject.CompareTag("Wall"))
        {
            Vector2 normal = obj.contacts[0].normal;//衝突点の法線
            Vector2 velocity = rigid2D.velocity;//現在の速度
            Vector2 velocityNext = Vector2.Reflect(velocity, normal);//反射ベクトル
            rigid2D.velocity = velocityNext.normalized * movespeed;
        }
    }
}
