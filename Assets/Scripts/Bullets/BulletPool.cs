using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet bulletprefab;
    [SerializeField] private Bullet_Base bullet_baseprefab;
    private int poolsize_p= 32;//プレイヤー用の弾のプールサイズ
    private int poolsize_b = 48;//基地用の弾のプールサイズ

    private Queue<Bullet> bulletpool;
    private Queue<Bullet_Base> bullet_basepool;

    //プールの初期化
    private void Awake()
    {
        bulletpool = new Queue<Bullet>();
        bullet_basepool = new Queue<Bullet_Base>();

        //Bulletのプールの初期化
        for(int i = 0;i < poolsize_p;i++)
        {
            Bullet instance = Instantiate(bulletprefab,transform);
            instance.gameObject.SetActive(false);
            instance.name = $"{instance.name}_{i}";
            instance.GetComponent<Bullet>().bulletPool = this;
            bulletpool.Enqueue(instance);
        }

        //Bullet_Baseのプールの初期化
        for(int i = 0;i < poolsize_b; i++)
        {
            Bullet_Base instance = Instantiate(bullet_baseprefab, transform);
            instance.gameObject.SetActive(false);
            instance.name = $"{instance.name}_{i}";
            instance.GetComponent<Bullet_Base>().bulletPool = this;
            bullet_basepool.Enqueue(instance);
        }
    }

    //プールから弾を取り出す（プレイヤー用の弾）
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

    //プールから弾を取り出す（基地用の弾）
    public Bullet_Base GetBulletBase(Vector3 pos, Quaternion rot, bool isRuby)
    {
        if (bullet_basepool.Count <= 0)
        {
            Debug.LogError("プール内にBullet_Baseが存在しません。");
            return null;
        }

        Bullet_Base bullet_Base = bullet_basepool.Dequeue();
        bullet_Base.gameObject.SetActive(true);

        if(isRuby) bullet_Base.isRuby_bullet = true;
        else bullet_Base.isRuby_bullet = false;

        return bullet_Base;
    }

    //プールに弾を戻す（プレイヤー用の弾）
    public void CollectBullet(Bullet _bullet)
    {
        _bullet.gameObject.SetActive(false);
        bulletpool.Enqueue(_bullet);
    }

    //プールに弾を戻す（基地用の弾）
    public void CollectBulletBase(Bullet_Base _bulletBase)
    {
        _bulletBase.gameObject.SetActive(false);
        bullet_basepool.Enqueue(_bulletBase);
    }
}
