using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet bulletprefab;
    private int poolsize = 32;

    private Queue<Bullet> bulletpool;

    //プールの初期化
    private void Awake()
    {
        bulletpool = new Queue<Bullet>();
        Bullet instance = null;

        for(int i = 0;i < poolsize;i++)
        {
            instance = Instantiate(bulletprefab);
            instance.gameObject.SetActive(false);
            instance.GetComponent<Bullet>().bulletPool = this;
            bulletpool.Enqueue(instance);
        }
    }

    //プールから弾を取り出す
    public Bullet GetBullet(Vector3 pos,Quaternion rot)
    {
        if(bulletpool.Count <= 0)
        {
            Debug.LogError("プール内にオブジェクトが存在しません。");
            return null;
        }
        Bullet bullet = bulletpool.Dequeue();
        bullet.gameObject.SetActive(true);
        bullet.BulletPosition(pos,rot);
        return bullet;
    }

    //プールに弾を戻す
    public void CollectBullet(Bullet _bullet)
    {
        _bullet.gameObject.SetActive(false);
        bulletpool.Enqueue(_bullet);
    }
}
