using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HpgaugeDisplay : MonoBehaviour
{
    [SerializeField] PlayerControl playerControl;
    GameObject hpgauge0,hpgauge1,hpgauge2,hpgauge3,hpgauge4,hpgauge5,
    hpgauge6,hpgauge7,hpgauge8,hpgauge9,hpgauge10;
    void Start()
    {
        hpgauge0 = transform.GetChild(0).gameObject;
        hpgauge1 = transform.GetChild(1).gameObject;
        hpgauge2 = transform.GetChild(2).gameObject;
        hpgauge3 = transform.GetChild(3).gameObject;
        hpgauge4 = transform.GetChild(4).gameObject;
        hpgauge5 = transform.GetChild(5).gameObject;
        hpgauge6 = transform.GetChild(6).gameObject;
        hpgauge7 = transform.GetChild(7).gameObject;
        hpgauge8 = transform.GetChild(8).gameObject;
        hpgauge9 = transform.GetChild(9).gameObject;
        hpgauge10 = transform.GetChild(10).gameObject;
    }

    void FixedUpdate()
    {
        Display();
    }

    private void Display()
    {
        if(playerControl.hp == 0)hpgauge0.SetActive(true);
        else hpgauge0.SetActive(false);
        
        if(playerControl.hp == 1)hpgauge1.SetActive(true);
        else hpgauge1.SetActive(false);

        if(playerControl.hp == 2)hpgauge2.SetActive(true);
        else hpgauge2.SetActive(false);

        if(playerControl.hp == 3)hpgauge3.SetActive(true);
        else hpgauge3.SetActive(false);

        if(playerControl.hp == 4)hpgauge4.SetActive(true);
        else hpgauge4.SetActive(false);

        if(playerControl.hp == 5)hpgauge5.SetActive(true);
        else hpgauge5.SetActive(false);

        if(playerControl.hp == 6)hpgauge6.SetActive(true);
        else hpgauge6.SetActive(false);

        if(playerControl.hp == 7)hpgauge7.SetActive(true);
        else hpgauge7.SetActive(false);

        if(playerControl.hp == 8)hpgauge8.SetActive(true);
        else hpgauge8.SetActive(false);

        if(playerControl.hp == 9)hpgauge9.SetActive(true);
        else hpgauge9.SetActive(false);

        if(playerControl.hp == 10)hpgauge10.SetActive(true);
        else hpgauge10.SetActive(false);
    }
}
