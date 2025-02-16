using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource audioSource;
    public AudioClip bgmClip; // BGMの音声ファイル

    void Awake()
    {
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // シーン間でオブジェクトを破棄しない
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>(); // AudioSourceコンポーネントを取得
    }

    void Start()
    {
        PlayBGM();
    }

    // BGMを再生するメソッド
    public void PlayBGM()
    {
        if (bgmClip != null)
        {
            audioSource.clip = bgmClip;
            audioSource.loop = true; // ループ再生を有効にする
            audioSource.Play();
        }
        else
        {
            Debug.LogError("BGMの音声ファイルが設定されていません。");
        }
    }

    // BGMを停止するメソッド
    public void StopBGM()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
