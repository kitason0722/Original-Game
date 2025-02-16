using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource audioSource;
    public AudioClip bgmClip; // BGM�̉����t�@�C��

    void Awake()
    {
        // �V���O���g���p�^�[���̎���
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �V�[���ԂŃI�u�W�F�N�g��j�����Ȃ�
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>(); // AudioSource�R���|�[�l���g���擾
    }

    void Start()
    {
        PlayBGM();
    }

    // BGM���Đ����郁�\�b�h
    public void PlayBGM()
    {
        if (bgmClip != null)
        {
            audioSource.clip = bgmClip;
            audioSource.loop = true; // ���[�v�Đ���L���ɂ���
            audioSource.Play();
        }
        else
        {
            Debug.LogError("BGM�̉����t�@�C�����ݒ肳��Ă��܂���B");
        }
    }

    // BGM���~���郁�\�b�h
    public void StopBGM()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
