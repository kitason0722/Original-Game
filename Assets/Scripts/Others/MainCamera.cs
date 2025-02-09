using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Start()
    {
        PlayerControl[] players = FindObjectsOfType<PlayerControl>();//生成されたプレイヤーを全て取得

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
        transform.position = new Vector3(player.position.x,player.position.y+2,-10f);
    }
}
