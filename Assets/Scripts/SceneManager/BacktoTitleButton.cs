using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoTitleButton : MonoBehaviour
{
    public void OnClick_BacktoTitleButton()
    {
        string currentscene = SceneManager.GetActiveScene().name;

        //ResultSceneからTitleSceneに戻る時のみタイトルBGMを再生する
        if(currentscene == "ResultScene") AudioManager.Instance.PlayBGM();

        SceneManager.LoadScene("TitleScene");
    }
}
