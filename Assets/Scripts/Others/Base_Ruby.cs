using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Ruby : MonoBehaviour
{
    public int hp = 30;//基地のHP
    private int hitcount = 0;//被弾した回数
    [SerializeField] private bool isRuby = true;//チームの判別
    [SerializeField] private float rad = 10.0f;//検知できる範囲
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private float shotinterval = 0.5f;//弾を打つまでのインターバル
    //各発射位置の弾を打つまでのインターバルのカウント変数
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

        //各発射位置に一定距離近づかれたら弾を発射

        //1つ目
        if (shottimer_1 > shotinterval)
        {
            
        }
        //2つ目
        else if (shottimer_2 > shotinterval)
        {
            
        }
        //3つ目
        else if (shottimer_3 > shotinterval)
        {
            
        }
        //4つ目
        else if (shottimer_4 > shotinterval)
        {
            
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
