using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Base : Bullet
{
    public override void Start()
    {
        bulletspeed = 10.0f;
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
        transform.rotation = rot;
    }

    public  override void Fire()
    {
        if (isRuby_bullet)//Ruby�`�[���̒e�̏ꍇ
        {
            Vector3 min = new Vector3();//�ŏ��������擾
            GameObject[] players = GameObject.FindGameObjectsWithTag("Sapphire");//Shppire�̃v���C���[���擾
            foreach (GameObject player in players)
            {
                Vector3 dis = (transform.position - player.transform.position);
                if (dis.magnitude < min.magnitude)
                {
                    min = dis;
                }
            }
            rigid2D.velocity = min * bulletspeed;//��ԋ߂��v���C���[�Ɍ������Ēǔ�
        }
        else//Sapphire�`�[���̒e�̏ꍇ
        {
            Vector3 min = new Vector3();//�ŏ��������擾
            GameObject[] players = GameObject.FindGameObjectsWithTag("Ruby");//Ruby�̃v���C���[���擾
            foreach (GameObject player in players)
            {
                Vector3 dis = (transform.position - player.transform.position);
                if (dis.magnitude < min.magnitude)
                {
                    min = dis;
                }
            }
            rigid2D.velocity = min * bulletspeed;//��ԋ߂��v���C���[�Ɍ������Ēǔ�
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
