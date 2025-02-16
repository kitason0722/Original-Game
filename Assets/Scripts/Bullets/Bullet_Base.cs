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
        if (isRuby_bullet)//Ruby�`�[���̒e�̏ꍇ
        {
            Vector3 min = new Vector3(99999,99999,99999);//�ŏ��������擾

            //���I��Sapphire�`�[���̃v���C���[���擾
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
            rigid2D.velocity = min.normalized * bulletspeed;//��ԋ߂��v���C���[�Ɍ������Ēǔ�
        }
        else//Sapphire�`�[���̒e�̏ꍇ
        {
            Vector3 min = new Vector3(99999,99999,99999);//�ŏ��������擾

            //���I��Ruby�`�[���̃v���C���[���擾
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
            rigid2D.velocity = min.normalized * bulletspeed;//��ԋ߂��v���C���[�Ɍ������Ēǔ�
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
