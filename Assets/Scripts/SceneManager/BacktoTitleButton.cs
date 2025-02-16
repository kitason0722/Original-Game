using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoTitleButton : MonoBehaviour
{
    public void OnClick_BacktoTitleButton()
    {
        string currentscene = SceneManager.GetActiveScene().name;

        //ResultScene‚©‚çTitleScene‚É–ß‚é‚Ì‚İƒ^ƒCƒgƒ‹BGM‚ğÄ¶‚·‚é
        if(currentscene == "ResultScene") AudioManager.Instance.PlayBGM();

        SceneManager.LoadScene("TitleScene");
    }
}
