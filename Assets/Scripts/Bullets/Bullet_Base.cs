using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Base : Bullet
{
    public override void Start()
    {
        bulletspeed = 8.0f;
        maxlifetime = 2.0f;
        base.Start();
    }
    
   public override void Update()
    {
        Fire();
        Delete();
    }

    public void BulletBasePosition(Vector3 pos, Quaternion rot)
    {
        transform.position = pos;
        if(!isRuby_bullet) transform.rotation = Quaternion.Euler(0, 0, rot.eulerAngles.z + 180);
        else transform.rotation = rot;
    }

    public  override void Fire()
    {
        if (isRuby_bullet)//Rubyチームの弾の場合
        {
            Vector3 min = new Vector3(99999,99999,99999);//最小距離を取得

            //動的にSapphireチームのプレイヤーを取得
            List<PlayerControl> enemies = new List<PlayerControl>();
            enemies.AddRange(PlayerSpawn_Sapphire.GetPlayers());

            foreach (PlayerControl enemy in enemies)
            {
                if (enemy == null) continue;
                Vector3 dis = (transform.position - enemy.transform.position);
                if (dis.magnitude < min.magnitude)
                {
                    min = dis;
                }
            }
            rigid2D.velocity = min.normalized * bulletspeed;//一番近いプレイヤーに向かって追尾
        }
        else//Sapphireチームの弾の場合
        {
            Vector3 min = new Vector3(99999,99999,99999);//最小距離を取得

            //動的にRubyチームのプレイヤーを取得
            List<PlayerControl> enemies = new List<PlayerControl>();
            enemies.AddRange(PlayerSpawn_Ruby.GetPlayers());

            foreach (PlayerControl enemy in enemies)
            {
                if (enemy == null) continue;
                Vector3 dis = (transform.position - enemy.transform.position);
                if (dis.magnitude < min.magnitude)
                {
                    min = dis;
                }
            }
            rigid2D.velocity = min.normalized * bulletspeed;//一番近いプレイヤーに向かって追尾
        }
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
