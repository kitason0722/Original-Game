using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BacktoTitleButton : MonoBehaviour
{
    public void OnClick_BacktoTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
    }
}
