using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerControl : MonoBehaviour
{
    public int hp = 10;//プレイヤーのHP
    private const float rotatespeed = 4.0f;//回転速度
    private const float movespeed = 3.0f;//移動速度
    private const float maxspped = 4.0f;//最高移動速度
    private Rigidbody2D rigid2D;
    enum State
    {
        Active,
        Idle,
        Dead
    }

    State state = State.Active;

    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        //HPゲージ確認用
        // if(Input.GetKeyDown(KeyCode.O)) if(hp > 0)hp--;
        // if(Input.GetKeyDown(KeyCode.P)) if(hp < 10)hp++;
    }

    private void FixedUpdate()
    {
        switch(state)
        {
            case State.Active:
            Move();
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
        float speed = rigid2D.velocity.magnitude;
        if(Input.GetKey(KeyCode.W))
            {
                rigid2D.AddForce(transform.up * movespeed);
                if(speed > maxspped) speed = maxspped;
                Debug.Log(speed);
            }
            if(Input.GetKey(KeyCode.S))
            {
                rigid2D.velocity *= 0.975f;
                Debug.Log(speed);
                if(speed < 0.01f)
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
}
