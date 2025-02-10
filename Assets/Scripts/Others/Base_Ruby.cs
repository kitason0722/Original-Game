using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Ruby : MonoBehaviour
{
    public int hp = 30;//Šî’n‚ÌHP
    private int hitcount = 0;//”í’e‚µ‚½‰ñ”
    private bool isRuby = true;//ƒ`[ƒ€‚Ì”»•Ê
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
