using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private PlayerControl player;
    private PlayerControl target;
    private int shotcount = 0;
    public AttackState(PlayerControl player,PlayerControl target)
    {
        this.player = player;
        this.target = target;
    }

    public void Enter()
    {
        player.shotinterval = 1.0f;
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
                Vector3 dir = (target.transform.position - player.transform.position).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                player.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                player.Shot();
            }
            player.interval = 0.0f;
            shotcount++;
        }

        if(shotcount >= 5)
        {
            player.stateMachine.TransitionTo(new MoveState(player));
        }

    }

    public void Exit()
    {

    }
}
