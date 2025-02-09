using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform player;

    private void Start()
    {
        PlayerControl[] players = FindObjectsOfType<PlayerControl>();//�������ꂽ�v���C���[��S�Ď擾

        //isPlayer��true�̃v���C���[���擾
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
            Debug.LogError("isPlayer��true�̃v���C���[��������܂���B");
        }
    }
    private void Update()
    {
        transform.position = new Vector3(player.position.x,player.position.y+2,-10f);
    }
}
