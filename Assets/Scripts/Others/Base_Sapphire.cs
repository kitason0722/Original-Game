using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Sapphire : MonoBehaviour
{
    public int hp = 30;//基地のHP
    private int hitcount = 0;//被弾した回数
    [SerializeField]private bool isRuby = false;//チームの判別
    [SerializeField] private float rad = 10.0f;//検知できる範囲
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float shotinterval = 0.5f;//弾を打つまでのインターバル
    private float shottimer = 0.0f;//弾を打つまでのインターバルのカウント変数

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
            //弾のチーム判別を行う
            Bullet bullet = obj.gameObject.GetComponent<Bullet>();

            if (bullet == null)
            {
                Debug.LogError("Bulletコンポーネントが見つかりません。");
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
