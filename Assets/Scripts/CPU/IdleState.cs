using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private PlayerControl player;
    private float idletime = 1.0f;
    private float idletimer;
    private bool attacktoBase;

    public IdleState(PlayerControl player)
    {
        this.player = player;
    }

    public void Enter()
    {
        idletimer = 0.0f;
        attacktoBase = false;
    }

    public void Update()
    {
        idletimer += Time.deltaTime;

        if (idletimer >= idletime)
        {
            //Šm—¦‚ÅUŒ‚‚·‚é‘ÎÛ‚ğ•ÏX
            attacktoBase = Random.value > 0.5f;

            // MoveState‚ÉˆÚs
            player.stateMachine.TransitionTo(new MoveState(player,attacktoBase));
        }
    }

    public void Exit()
    {

    }
}
