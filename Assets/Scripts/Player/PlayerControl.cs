using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControl : MonoBehaviour
{
    public int hp = 10;//プレイヤーのHP
    private const float rotatespeed = 3.0f;//回転速度
    private const float movespeed = 1.5f;//移動速度
    private const float maxspped = 6.0f;//最高移動速度
    private float interval = 0.0f;//弾を打つまでのインターバルのカウント変数
    private float shotinterval = 0.5f;//弾を打つまでのインターバル
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
    private Rigidbody2D rigid2D;

     private enum State//プレイヤーの状態
    {
        Active,
        Idle,
        Dead
    }

    private State state = State.Active;

    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        dir = transform.up.normalized;//プレイヤーの向いている方向を常に取得
        interval += Time.deltaTime;//時間の計測

        //プレイヤーの処理
        if(isPlayer)
        {
            //HPゲージ確認用
            if (Input.GetKeyDown(KeyCode.O)) if (hp > 0) hp--;
            if (Input.GetKeyDown(KeyCode.P)) if (hp < 10) hp++;

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
                //Move();
                //if(Input.GetKeyDown(KeyCode.Space))
                //{
                //    if(interval > shotinterval)
                //    {
                //        Shot();
                //        interval = 0.0f;
                //    }
                //}
                break;

                case State.Dead:
                _deathinterval += Time.deltaTime;
                if(_deathinterval > deathinterval)
                {
                    _deathinterval = 0.0f;
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

    protected void Shot()
    {
        bulletPool.GetBullet(bulletposition.transform.position,transform.rotation,isRuby);
    }

    public void SetBulletPool(BulletPool pool)
    {
        bulletPool = pool;
    }

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
                if (hp <= 0)
                {
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
