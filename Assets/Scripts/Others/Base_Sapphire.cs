using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Sapphire : MonoBehaviour
{
    public int hp = 30;//��n��HP
    private int hitcount = 0;//��e������
    [SerializeField]private bool isRuby = false;//�`�[���̔���
    [SerializeField] private float rad = 10.0f;//���m�ł���͈�
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float shotinterval = 0.5f;//�e��ł܂ł̃C���^�[�o��
    private float shottimer = 0.0f;//�e��ł܂ł̃C���^�[�o���̃J�E���g�ϐ�

    void Start()
    {
        
    }
    
    void Update()
    {
        shottimer += Time.deltaTime;
        if(shottimer > shotinterval)
        {
            foreach(Transform playerspos in PlayerSpawn_Ruby.playerspos)
            {
                float distance = Vector3.Distance(transform.position,playerspos.position);
                if(distance <= rad)
                {
                    bulletPool.GetBullet(playerspos.position, Quaternion.identity, isRuby);
                    shottimer = 0.0f;
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Bullet"))
        {
            //�e�̃`�[�����ʂ��s��
            Bullet bullet = obj.gameObject.GetComponent<Bullet>();

            if (bullet == null)
            {
                Debug.LogError("Bullet�R���|�[�l���g��������܂���B");
                return;
            }

            if (bullet.isRuby_bullet)
            {
                hitcount++;
                if(hitcount == 2)
                {
                    hp -= 1;
                    hitcount = 0;
                }
            }
        }
    }
}
