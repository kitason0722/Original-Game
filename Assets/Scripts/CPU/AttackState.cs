using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private PlayerControl player;
    private Transform target;
    private int shotcount = 0;
    private int currenthp;
    public AttackState(PlayerControl player,Transform target)
    {
        this.player = player;
        this.target = target;
    }

    public void Enter()
    {
        player.shotinterval = 1.0f;
        currenthp = player.hp;
    }

    public void Update()
    {
        player.interval += Time.deltaTime;
        if(player.interval > player.shotinterval)
        {
            // UŒ‚ó‘Ô‚Ì‚Æ‚«‚Ìˆ—
            if (target != null)
            {
                // ƒ^[ƒQƒbƒg‚Ì•ûŒü‚ÉŒü‚©‚Á‚Ä’e‚ðŒ‚‚Â
                Vector3 dir = (target.position - player.transform.position).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                player.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                player.Shot();
            }
            player.interval = 0.0f;
            shotcount++;
        }

        //3‰ñUŒ‚‚·‚éA‚à‚µ‚­‚Í2ƒ_ƒ[ƒWˆÈãŽó‚¯‚½‚ç‘Ò‹@ó‘Ô‚É‘JˆÚ
        if (shotcount >= 3 || (currenthp - player.hp) >= 2)
        {
            player.stateMachine.TransitionTo(new IdleState(player));
        }

    }

    public void Exit()
    {

    }
}
