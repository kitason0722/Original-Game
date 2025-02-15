using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StateMachine
{
    public IState currentState;

    public void Init_State(IState startState)
    {
        currentState = startState;
        startState.Enter();
    }

    public void TransitionTo(IState nextState)
    {
        if(currentState != null)
        {
            currentState.Exit();
        }
        
        currentState = nextState;
        nextState.Enter();
    }

    public void Update()
    {
        if(currentState != null)
        {
            currentState.Update();
        }
    }
}
