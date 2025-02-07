using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControl : MonoBehaviour
{
    public int hp = 10;//プレイヤーのHP
    private const float rotatespeed = 1.5f;//回転速度
    private const float movespeed = 1.5f;//移動速度
    private const float maxspped = 6.0f;//最高移動速度
    public Vector3 dir;//プレイヤーが向いている方向
    [SerializeField] BulletPool bulletPool;
    public GameObject bulletposition;
    private Rigidbody2D rigid2D;
    enum State
    {
        Active,
        Idle,
        Dead
    }

    State state = State.Active;

    void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //HPゲージ確認用
        // if(Input.GetKeyDown(KeyCode.O)) if(hp > 0)hp--;
        // if(Input.GetKeyDown(KeyCode.P)) if(hp < 10)hp++;

        //プレイヤーの向いている方向を常に取得
        dir = transform.up.normalized;

        switch(state)
        {
            case State.Active:
            Move();
            Shot();
            break;

            case State.Dead:
            break;

            case State.Idle:
            break;
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
            Debug.Log("機体のスピード:" + rigid2D.velocity.magnitude);
        }
        if(Input.GetKey(KeyCode.S))
        {
            rigid2D.velocity *= 0.985f;
            if(rigid2D.velocity.magnitude < 0.01f)
            {
                rigid2D.velocity = Vector2.zero;//完全停止
            }
            Debug.Log("機体のスピード:" + rigid2D.velocity.magnitude);
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

    private void Shot()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            bulletPool.GetBullet(bulletposition.transform.position,transform.rotation);
        }
    }

    protected void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.CompareTag("Bullet"))hp -= 1;

        if(obj.gameObject.CompareTag("Wall"))
        {
            Vector2 normal = obj.contacts[0].normal;//衝突点の法線
            Vector2 velocity = rigid2D.velocity;//現在の速度
            Vector2 velocityNext = Vector2.Reflect(velocity, normal);//反射ベクトル
            rigid2D.velocity = velocityNext.normalized * movespeed;
        }
    }
}
