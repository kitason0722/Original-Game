using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        GetPlayer();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x,player.position.y,-10f);
    }

    private void GetPlayer()
    {
        List<PlayerControl> players = new List<PlayerControl>();//Rubyチームのプレイヤーを取得
        players.AddRange(PlayerSpawn_Ruby.GetPlayers());

        //isPlayerがtrueのプレイヤーを取得
        foreach (PlayerControl p in players)
        {
            if(p == null)
            {
                Debug.LogError("プレイヤーが見つかりません。");
                continue;
            }

            if (p.isPlayer)
            {
                this.player = p.transform;
                Debug.Log("プレイヤーを取得しました。");
                break;
            }
        }

        if (player == null)
        {
            Debug.LogError("isPlayerがtrueのプレイヤーが見つかりません。");
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GetPlayer();
    }
}
