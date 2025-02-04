using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform player;
    private void Update()
    {
        transform.position = new Vector3(player.position.x,player.position.y,-10f);
    }
}
