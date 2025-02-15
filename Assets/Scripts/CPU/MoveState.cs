using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private PlayerControl player;
    private GameObject Base;
    private float keepdistance = 5.0f;//基地もしくは敵との間に保ちたい距離
    private float attacktime = 0.3f;//攻撃間隔
    private float attacktimer = 0.0f;
    private bool attacktoBase;

    public MoveState(PlayerControl player,bool target)
    {
        this.player = player;
        attacktoBase = target;
    }

    public void Enter()
    {
        //目的の基地を取得
        if (player.isRuby)Base = GameObject.Find("Base_Sapphire");
        else Base = GameObject.Find("Base_Ruby");
    }

    public void Update()
    {
        //敵の位置を取得
        List<PlayerControl> enemies = new List<PlayerControl>();
        if (player.isRuby) enemies.AddRange(PlayerSpawn_Sapphire.GetPlayers());
        else enemies.AddRange(PlayerSpawn_Ruby.GetPlayers());

        //一番近い敵までの距離を取得
        float toenemy_min = float.MaxValue;
        PlayerControl nearestenemy = null;

        foreach (PlayerControl enemy in enemies)
        {
            float toenemy = (player.transform.position - enemy.transform.position).sqrMagnitude;
            if (toenemy < toenemy_min)
            {
                toenemy_min = toenemy;
                nearestenemy = enemy;
            }
        }

        //基地までの距離を取得
        float toBase = (player.transform.position - Base.transform.position).sqrMagnitude;

        //近い方の向きを取得
        Vector3 dir;

        if (attacktoBase)
        {
            if (toBase < keepdistance)
            {
                //基地から離れる方向に移動
                dir = (player.transform.position - Base.transform.position).normalized;
                dir = Quaternion.Euler(0, 0, 45) * dir;
            }
            else if (toBase > keepdistance)
            {
                //基地に近づく方向に移動
                dir = (Base.transform.position - player.transform.position).normalized;
                attacktimer += Time.deltaTime;
                if (attacktimer > attacktime)
                {
                    player.stateMachine.TransitionTo(new AttackState(player, Base.transform));
                    return;
                }
            }
            else
            {
                //現在の位置を維持
                dir = player.transform.up;
            }
        }
        else
        {
            if (nearestenemy != null)
            {
                float distancetoEnemy = Mathf.Sqrt(toenemy_min);
                if (distancetoEnemy < keepdistance)
                {
                    // 敵から離れる方向に移動
                    dir = (player.transform.position - nearestenemy.transform.position).normalized;
                    dir = Quaternion.Euler(0, 0, 45) * dir;

                    attacktimer += Time.deltaTime;
                    if (attacktimer > attacktime)
                    {
                        player.stateMachine.TransitionTo(new AttackState(player, nearestenemy.transform));
                        return;
                    }
                }
                else if (distancetoEnemy > keepdistance)
                {
                    attacktimer = 0.0f;
                    // 敵に近づく方向に移動
                    dir = (nearestenemy.transform.position - player.transform.position).normalized;
                }
                else
                {
                    // 現在の位置を維持
                    dir = player.transform.up;
                }

                //プレイヤーの向きを変更
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                player.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

                //プレイヤーを移動
                player.rigid2D.AddForce(player.transform.up * player.movespeed);
                if (player.rigid2D.velocity.magnitude > player.maxspped)
                {
                    player.rigid2D.velocity = player.rigid2D.velocity.normalized * player.maxspped;
                }
            }
        }
    }

    public void Exit()
    {

    }

}
