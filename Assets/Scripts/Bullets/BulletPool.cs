using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet bulletprefab;
    [SerializeField] private Bullet_Base bullet_baseprefab;
    private int poolsize = 32;

    private Queue<Bullet> bulletpool;
    private Queue<Bullet_Base> bullet_basepool;

    //プールの初期化
    private void Awake()
    {
        bulletpool = new Queue<Bullet>();
        bullet_basepool = new Queue<Bullet_Base>();

        //Bulletのプールの初期化
        for(int i = 0;i < poolsize;i++)
        {
            Bullet instance = Instantiate(bulletprefab,transform);
            instance.gameObject.SetActive(false);
            instance.name = $"{instance.name}_{i}";
            instance.GetComponent<Bullet>().bulletPool = this;
            bulletpool.Enqueue(instance);
        }

        //Bullet_Baseのプールの初期化（後で追加）

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
