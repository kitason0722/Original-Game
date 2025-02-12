using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Base : Bullet
{
    private Transform target;
    public override void Start()
    {
        bulletspeed = 10.0f;
        maxlifetime = 2.0f;
        base.Start();
    }
    
   public override void Update()
    {
        if(target != null)Fire();
        Delete();
    }

    public void BulletBasePosition(Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        transform.rotation = rot;
    }

    public void Fire(Transform playerpos)
    {
        target = playerpos;
        if(target == null)
        {
            Debug.LogError("target‚ªnull‚Å‚·");
            return;
        }

        Vector3 dir = (target.position - transform.position).normalized;
        rigid2D.velocity = dir * bulletspeed;
    }

    public override void Delete()
    {
        lifetime += Time.deltaTime;
        if (lifetime > maxlifetime)
        {
            bulletPool.CollectBulletBase(this);
            lifetime = 0.0f;
        }
    }

    protected override void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Wall"))
        {
            bulletPool.CollectBulletBase(this);
        }
        else bulletPool.CollectBulletBase(this);
    }
}
