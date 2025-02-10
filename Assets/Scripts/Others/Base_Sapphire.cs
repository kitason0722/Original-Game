using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Sapphire : MonoBehaviour
{
    public int hp = 30;//基地のHP
    private int hitcount = 0;//被弾した回数
    private bool isRuby = false;//チームの判別
    
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
