using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoTitleButton : MonoBehaviour
{
    public void OnClick_BacktoTitleButton()
    {
        string currentscene = SceneManager.GetActiveScene().name;

        //ResultScene����TitleScene�ɖ߂鎞�̂݃^�C�g��BGM���Đ�����
        if(currentscene == "ResultScene") AudioManager.Instance.PlayBGM();

        SceneManager.LoadScene("TitleScene");
    }
}
