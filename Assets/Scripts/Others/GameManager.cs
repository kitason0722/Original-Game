using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//ゲーム進行とUIの管理をしたい
public class GameManager : MonoBehaviour
{
    private GameObject Timer;//タイマーUI
    Text timertext;//タイマーのテキスト
    private float timer = 120;//ゲームの制限時間
    private float idletime = 2.0f;//ゲーム終了後の待機時間

    private void Start()
    {
        this.Timer = GameObject.Find("Timer");
        this.timertext = this.Timer.GetComponent<Text>();

        if(Timer == null)
        {
            Debug.LogError("Timerオブジェクトが見つかりません。");
        }

        if (timertext == null)
        {
            Debug.LogError("TimerオブジェクトにTextコンポーネントが見つかりません。");
        }
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
        {
            this.Timer.GetComponent<Text>().text = timer.ToString("F0");
            if (timer <= 10)
            {
                this.timertext.fontSize = 150;
                this.timertext.fontStyle = FontStyle.Bold;
            }
        }
        
        else
        {
            Gameover();
        }
    }

    private void Gameover()
    {
        idletime -= Time.deltaTime;
        if (idletime < 0)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
