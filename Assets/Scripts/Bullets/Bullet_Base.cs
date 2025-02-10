using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Base : Bullet
{
    
    private void Start()
    {
        base.Start();
        maxlifetime = 2.0f;
    }
    
   private void Update()
    {
        Fire();
        base.Delete();
    }

    public override void Fire()
    {
        base.Fire();
    }

    protected override void OnCollisionEnter2D(Collision2D obj)
    {
        base.OnCollisionEnter2D(obj);
    }
}
