using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Sapphire : MonoBehaviour
{
    public int hp = 30;//��n��HP
    private int hitcount = 0;//��e������
    private bool isRuby = false;//�`�[���̔���
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
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
