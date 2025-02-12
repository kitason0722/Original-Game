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
    //�e���ˈʒu�̒e��ł܂ł̃C���^�[�o���̃J�E���g�ϐ�
    private float shottimer_1 = 0.0f,shottimer_2 = 0.0f,shottimer_3 = 0.0f,shottimer_4 = 0.0f;

    public GameObject[] bulletposition = new GameObject[4];

    void Start()
    {
        
    }
    
    void Update()
    {
        shottimer_1 += Time.deltaTime;
        shottimer_2 += Time.deltaTime;
        shottimer_3 += Time.deltaTime;
        shottimer_4 += Time.deltaTime;

        //���I��Ruby�`�[���̃v���C���[���擾
        GameObject[] players = GameObject.FindGameObjectsWithTag("Ruby");
        
        //�e���ˈʒu�Ɉ�苗���߂Â��ꂽ��e�𔭎�
        //1��
        if (shottimer_1 > shotinterval)
        {
            foreach(GameObject player in players)
            {
                float dis = (bulletposition[0].transform.position - player.transform.position).sqrMagnitude;
                if (dis <= rad)
                {
                    bulletPool.GetBulletBase(bulletposition[0].transform.position, Quaternion.identity, isRuby);
                    shottimer_1 = 0.0f;
                    break;
                }
            }
        }
        //2��
        else if (shottimer_2 > shotinterval)
        {
            foreach (GameObject player in players)
            {
                float dis = (bulletposition[1].transform.position - player.transform.position).sqrMagnitude;
                if (dis <= rad)
                {
                    bulletPool.GetBulletBase(bulletposition[1].transform.position, Quaternion.identity, isRuby);
                    shottimer_2 = 0.0f;
                    break;
                }
            }
        }
        //3��
        else if (shottimer_3 > shotinterval)
        {
            foreach (GameObject player in players)
            {
                float dis = (bulletposition[2].transform.position - player.transform.position).sqrMagnitude;
                if (dis <= rad)
                {
                    bulletPool.GetBulletBase(bulletposition[2].transform.position, Quaternion.identity, isRuby);
                    shottimer_3 = 0.0f;
                    break;
                }
            }
        }
        //4��
        else if (shottimer_4 > shotinterval)
        {
            foreach (GameObject player in players)
            {
                float dis = (bulletposition[3].transform.position - player.transform.position).sqrMagnitude;
                if (dis <= rad)
                {
                    bulletPool.GetBulletBase(bulletposition[3].transform.position, Quaternion.identity, isRuby);
                    shottimer_4 = 0.0f;
                    break;
                }
            }
        }
    }
    //�Փˎ��̏���
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
            //2��e�ɂ�1�_���[�W�󂯂�悤�ɂ���
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
