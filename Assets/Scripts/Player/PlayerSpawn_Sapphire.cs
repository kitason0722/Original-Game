using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn_Sapphire : MonoBehaviour
{
    [SerializeField] private GameObject playerprefab;//プレイヤーのプレハブ
    [SerializeField] private BulletPool bulletPool;//弾のプール
    [SerializeField] private float radius = 3.0f;//プレイヤーの生成半径
    private int playersize = 4;//プレイヤーの数
    private void Awake()
    {
        for (int i = 0; i < playersize; i++)
        {
            //スポーンエリア内の範囲内のランダムな位置を取得
            Vector2 randompos = Random.insideUnitCircle * radius;
            Vector3 spawnpos = new Vector3(randompos.x, randompos.y, 0) + transform.position;

            //プレイヤーの生成
            GameObject instance = Instantiate(playerprefab, spawnpos, transform.rotation,transform);
            instance.name = $"{playerprefab.name}_{i}";

            //ローカルスケールをリセット
            instance.transform.localScale = Vector3.one;

            // ワールドスケールをリセット
            instance.transform.SetParent(null, true);
            instance.transform.localScale = playerprefab.transform.localScale;
            instance.transform.SetParent(transform, true);

            //プレイヤーの変数の設定
            PlayerControl playerControl = instance.GetComponent<PlayerControl>();
            playerControl.team = PlayerControl.Team.Sapphire;
            playerControl.isPlayer = false;

            //プレイヤーを生成した後に弾のプールをセット
            if (playerControl != null)
            {
                playerControl.SetBulletPool(bulletPool);
            }
            else
            {
                Debug.LogError("PlayerControlコンポーネントが見つかりません。");
            }
        }
    }
}
