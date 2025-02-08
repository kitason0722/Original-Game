using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    [SerializeField] private GameObject playerprefab;
    private int playersize = 4;//プレイヤーの数
    private void Awake()
    {
        for(int i = 0;i < playersize;i++)
        {
            GameObject instance = Instantiate(playerprefab,transform);
            instance.name = $"{playerprefab.name}_{i}";

            PlayerControl playerControl = instance.GetComponent<PlayerControl>();
            if(i==0)playerControl.isPlayer = true;
            else playerControl.isPlayer = false;
            
            instance.SetActive(false);
        }
    }
}
