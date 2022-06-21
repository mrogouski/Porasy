using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IBaseState
{
    public void EnterState(StateManager stateManager)
    {
        Debug.Log("Current State: Attack");
        throw new System.NotImplementedException();
    }

    public void ExitState(StateManager stateManager)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(StateManager stateManager)
    {
        throw new System.NotImplementedException();
    }
}
