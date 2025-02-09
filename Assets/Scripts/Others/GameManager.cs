using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//�Q�[���i�s��UI�̊Ǘ���������
public class GameManager : MonoBehaviour
{
    private GameObject Timer;//�^�C�}�[UI
    Text timertext;//�^�C�}�[�̃e�L�X�g
    private float timer = 120;//�Q�[���̐�������
    private float idletime = 2.0f;//�Q�[���I����̑ҋ@����

    private void Start()
    {
        this.Timer = GameObject.Find("Timer");
        this.timertext = this.Timer.GetComponent<Text>();

        if(Timer == null)
        {
            Debug.LogError("Timer�I�u�W�F�N�g��������܂���B");
        }

        if (timertext == null)
        {
            Debug.LogError("Timer�I�u�W�F�N�g��Text�R���|�[�l���g��������܂���B");
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
