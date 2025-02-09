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
            instance = Instantiate(bulletprefab,transform);
            instance.gameObject.SetActive(false);
            instance.name = $"{instance.name}_{i}";
            instance.GetComponent<Bullet>().bulletPool = this;
            bulletpool.Enqueue(instance);
        }
    }

    //プールから弾を取り出す
    public Bullet GetBullet(Vector3 pos,Quaternion rot,bool isRuby)
    {
        if(bulletpool.Count <= 0)
        {
            Debug.LogError("プール内に弾が存在しません。");
            return null;
        }
        Bullet bullet = bulletpool.Dequeue();
        bullet.gameObject.SetActive(true);

        bullet.BulletPosition(pos,rot);
        if(isRuby) bullet.isRuby_bullet = true;
        else bullet.isRuby_bullet = false;

        return bullet;
    }

    //プールに弾を戻す
    public void CollectBullet(Bullet _bullet)
    {
        _bullet.gameObject.SetActive(false);
        bulletpool.Enqueue(_bullet);
    }
}
