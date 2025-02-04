using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField]int hp = 10;//プレイヤーのHP
    private const float rotatespeed = 4.0f;//回転速度
    private const float movespeed = 3.0f;//移動速度
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
                speed = rigid2D.velocity.magnitude;
                Debug.Log(speed);
            }
            if(Input.GetKey(KeyCode.S))
            {
                rigid2D.velocity *= 0.98f;
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
