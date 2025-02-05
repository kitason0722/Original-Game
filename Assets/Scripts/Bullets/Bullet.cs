using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    protected float bulletspeed = 10.0f;//弾速
    private float lifetime = 0.0f;//弾の生存時間
    private const float maxlifetime = 2.0f;//弾の最大生存時間
    private float interval = 0.5f;//弾の発射のインターバル
    private Rigidbody2D rigid2D;
    public BulletPool bulletPool;
    void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Fire();
        Delete();
    }

    public void BulletPosition(Vector3 pos,Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
    }

    //弾の撃ち出し
    public void Fire()
    {
        interval += Time.deltaTime;
        if(interval >= 0.5f)
        {
            rigid2D.velocity = transform.up.normalized * bulletspeed;
            interval = 0.0f;
        }
    }

    //弾の削除
    public void Delete()
    {
        lifetime += Time.deltaTime;
        if(lifetime > maxlifetime)
        {
            bulletPool.CollectBullet(this);
            lifetime = 0.0f;
        }
    }
}
