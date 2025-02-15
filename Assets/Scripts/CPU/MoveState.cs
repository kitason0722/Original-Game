using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IState
{
    private PlayerControl player;
    private GameObject Base;
    private float keepdistance = 5.0f;//��n�������͓G�Ƃ̊Ԃɕۂ���������
    private float attacktime = 0.3f;//�U���Ԋu
    private float attacktimer = 0.0f;
    private bool attacktoBase;

    public MoveState(PlayerControl player,bool target)
    {
        this.player = player;
        attacktoBase = target;
    }

    public void Enter()
    {
        //�ړI�̊�n���擾
        if (player.isRuby)Base = GameObject.Find("Base_Sapphire");
        else Base = GameObject.Find("Base_Ruby");
    }

    public void Update()
    {
        //�G�̈ʒu���擾
        List<PlayerControl> enemies = new List<PlayerControl>();
        if (player.isRuby) enemies.AddRange(PlayerSpawn_Sapphire.GetPlayers());
        else enemies.AddRange(PlayerSpawn_Ruby.GetPlayers());

        //��ԋ߂��G�܂ł̋������擾
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

        //��n�܂ł̋������擾
        float toBase = (player.transform.position - Base.transform.position).sqrMagnitude;

        //�߂����̌������擾
        Vector3 dir;

        if (attacktoBase)
        {
            if (toBase < keepdistance)
            {
                //��n���痣�������Ɉړ�
                dir = (player.transform.position - Base.transform.position).normalized;
                dir = Quaternion.Euler(0, 0, 45) * dir;
            }
            else if (toBase > keepdistance)
            {
                //��n�ɋ߂Â������Ɉړ�
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
                //���݂̈ʒu���ێ�
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
                    // �G���痣�������Ɉړ�
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
                    // �G�ɋ߂Â������Ɉړ�
                    dir = (nearestenemy.transform.position - player.transform.position).normalized;
                }
                else
                {
                    // ���݂̈ʒu���ێ�
                    dir = player.transform.up;
                }

                //�v���C���[�̌�����ύX
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                player.transform.rotation = Quaternion.Euler(0, 0, angle - 90);

                //�v���C���[���ړ�
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
