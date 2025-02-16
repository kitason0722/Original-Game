using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Start()
    {
        List<PlayerControl> players = new List<PlayerControl>();//Rubyチームのプレイヤーを取得
        players.AddRange(PlayerSpawn_Ruby.GetPlayers());

        //isPlayerがtrueのプレイヤーを取得
        foreach (PlayerControl p in players)
        {
            if (p.isPlayer)
            {
                this.player = p.transform;
                break;
            }
        }

        if(player == null)
        {
            Debug.LogError("isPlayerがtrueのプレイヤーが見つかりません。");
        }
    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x,player.position.y,-10f);
    }
}
