using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultTextManager : MonoBehaviour
{
    private bool isRubyWin,draw;
    private void Start()
    {
        //リザルト画面のテキストを取得
        GameObject Ruby_Win = GameObject.Find("Ruby_Win");
        GameObject Sapphire_Win = GameObject.Find("Sapphire_Win");
        GameObject Draw = GameObject.Find("Draw");

        //一旦非表示にする
        Ruby_Win.SetActive(false);
        Sapphire_Win.SetActive(false);
        Draw.SetActive(false);

        //GameManagerからゲーム結果を取得
        isRubyWin = PlayerPrefs.GetInt("isRubyWin",0) == 1;
        draw = PlayerPrefs.GetInt("draw", 0) == 1;

        //ゲーム結果に応じて適切なリザルトを表示
        if (isRubyWin && !draw)
        {
            Ruby_Win.SetActive(true);
        }
        else if (!isRubyWin && !draw)
        {
            Sapphire_Win.SetActive(true);
        }
        else
        {
            Draw.SetActive(true);
        }
    }
}
