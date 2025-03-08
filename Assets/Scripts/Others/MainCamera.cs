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
        List<PlayerControl> players = new List<PlayerControl>();//Ruby�`�[���̃v���C���[���擾
        players.AddRange(PlayerSpawn_Ruby.GetPlayers());

        //isPlayer��true�̃v���C���[���擾
        foreach (PlayerControl p in players)
        {
            if(p == null)
            {
                Debug.LogError("�v���C���[��������܂���B");
                continue;
            }

            if (p.isPlayer)
            {
                this.player = p.transform;
                Debug.Log("�v���C���[���擾���܂����B");
                break;
            }
        }

        if (player == null)
        {
            Debug.LogError("isPlayer��true�̃v���C���[��������܂���B");
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
