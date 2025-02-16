using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    protected float bulletspeed = 10.0f;//弾速
    protected float lifetime = 0.0f;//弾の生存時間
    protected float maxlifetime = 1.5f;//弾の最大生存時間
    public bool isRuby_bullet = true;//チームの判別
    protected Rigidbody2D rigid2D;
    public BulletPool bulletPool;
    public virtual void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
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
    public virtual void Fire()
    {
        rigid2D.velocity = transform.up.normalized * bulletspeed;
    }

    //弾の削除
    public virtual void Delete()
    {
        lifetime += Time.deltaTime;
        if(lifetime > maxlifetime)
        {
            bulletPool.CollectBullet(this);
            lifetime = 0.0f;
        }
    }

    //衝突時の処理
    protected virtual void OnCollisionEnter2D(Collision2D obj)
    {
        if(obj.gameObject.CompareTag("Wall"))
        {
            bulletPool.CollectBullet(this);
        }
        else bulletPool.CollectBullet(this);
    }
}
