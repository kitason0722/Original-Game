using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//�Q�[���i�s��UI�̊Ǘ���������
public class GameManager : MonoBehaviour
{
    private GameObject Timer;//�^�C�}�[UI
    private GameObject Hp_Ruby, Hp_Sapphire;//HP��UI
    private Text timertext;//�^�C�}�[�̃e�L�X�g
    int hp_ruby, hp_sapphire;//HP�̐��l
    private float timer = 120;//�Q�[���̐�������
    private float idletime = 2.0f;//�Q�[���I����̑ҋ@����
    public bool isRubyWin = false;//Ruby�`�[���̏�������
    public bool draw = false;//������������

    private AudioSource audioSource;
    public AudioClip bgm_game;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        this.Timer = GameObject.Find("Timer");
        this.timertext = this.Timer.GetComponent<Text>();

        this.Hp_Ruby = GameObject.Find("HP_Ruby");
        this.Hp_Sapphire = GameObject.Find("HP_Sapphire");
    }
    private void Start()
    {
        PlayBGM();
    }
    private void Update()
    {
        hp_ruby = GameObject.Find("Base_Ruby").GetComponent<Base_Ruby>().hp;
        hp_sapphire = GameObject.Find("Base_Sapphire").GetComponent<Base_Sapphire>().hp;

        //�^�C�}�[�̏���
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

        //HP�\���̏���
        this.Hp_Ruby.GetComponent<Text>().text = hp_ruby.ToString();
        this.Hp_Sapphire.GetComponent<Text>().text = hp_sapphire.ToString();

        //�f�o�b�O�p
        //if(Input.GetKey(KeyCode.Y))GameObject.Find("Base_Ruby").GetComponent<Base_Ruby>().hp = 0;
        //if(Input.GetKey(KeyCode.U))GameObject.Find("Base_Sapphire").GetComponent<Base_Sapphire>().hp = 0;
        if (Input.GetKey(KeyCode.I))
        {
            draw = true;
            Gameover();
        }

        if (hp_ruby <= 0)
        {
            isRubyWin = false;
            Gameover();
        }
        else if(hp_sapphire <= 0)
        {
            isRubyWin = true;
            Gameover();
        }
    }

    private void Gameover()
    {
        idletime -= Time.deltaTime;
        if (idletime < 0)
        {
            if (hp_ruby > hp_sapphire)
            {
                isRubyWin = true;
            }
            else if (hp_ruby < hp_sapphire)
            {
                isRubyWin = false;
            }
            else
            {
                draw = true;
            }

            PlayerPrefs.SetInt("isRubyWin", isRubyWin ? 1 : 0);
            PlayerPrefs.SetInt("draw", draw ? 1 : 0);
            PlayerPrefs.Save();
            SceneManager.LoadScene("ResultScene");
        }
    }

    //BGM�̊Ǘ�
    public void PlayBGM()
    {
        audioSource.clip = bgm_game;
        audioSource.loop = true;
        audioSource.Play();
    }
}
