using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public void OnClick_StartButton()
    {
        AudioManager.Instance.StopBGM();
        SceneManager.LoadScene("GameScene");
    }
}
