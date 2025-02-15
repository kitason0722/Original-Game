using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Ruby : MonoBehaviour
{
    public int hp = 30;//��n��HP
    private int hitcount = 0;//��e������
    [SerializeField] private bool isRuby = true;//�`�[���̔���
    [SerializeField] private float rad = 100.0f;//���m�ł���͈�
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float shotinterval = 0.5f;//�e��ł܂ł̃C���^�[�o��
    //�e���ˈʒu�̒e��ł܂ł̃C���^�[�o���̃J�E���g�ϐ�
    private float shottimer_1 = 0.0f, shottimer_2 = 0.0f, shottimer_3 = 0.0f, shottimer_4 = 0.0f;

    public GameObject[] bulletposition = new GameObject[4];

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shottimer_1 += Time.deltaTime;
        shottimer_2 += Time.deltaTime;
        shottimer_3 += Time.deltaTime;
        shottimer_4 += Time.deltaTime;

        //���I��Sapphire�`�[���̃v���C���[���擾
        List<PlayerControl> enemies = new List<PlayerControl>();
        enemies.AddRange(PlayerSpawn_Sapphire.GetPlayers());

        //�e���ˈʒu�Ɉ�苗���߂Â��ꂽ��e�𔭎�
        //1��
        if (shottimer_1 > shotinterval)
        {
            foreach (PlayerControl enemy in enemies)
            {
                float dis = (bulletposition[0].transform.position - enemy.transform.position).sqrMagnitude;
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
            foreach (PlayerControl enemy in enemies)
            {
                float dis = (bulletposition[1].transform.position - enemy.transform.position).sqrMagnitude;
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
            foreach (PlayerControl enemy in enemies)
            {
                float dis = (bulletposition[2].transform.position - enemy.transform.position).sqrMagnitude;
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
            foreach (PlayerControl enemy in enemies)
            {
                float dis = (bulletposition[3].transform.position - enemy.transform.position).sqrMagnitude;
                if (dis <= rad)
                {
                    bulletPool.GetBulletBase(bulletposition[3].transform.position, Quaternion.identity, isRuby);
                    shottimer_4 = 0.0f;
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D obj)
    {
        if (obj.gameObject.CompareTag("Bullet"))
        {

            if (obj.gameObject.GetComponent<Bullet>().isRuby_bullet == false)
            {
                hitcount++;
                if(hitcount == 2)
                {
                    hp--;
                    hitcount = 0;
                }
            }
        }
    }
}
