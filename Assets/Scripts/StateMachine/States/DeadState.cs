using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : IBaseState
{
    public void EnterState(StateManager stateManager)
    {
        Debug.Log("Current State: Death");
        stateManager.animator.SetTrigger(stateManager.DeathHash);
    }

    public void ExitState(StateManager stateManager)
    {
        //throw new System.NotImplementedException();
    }

    public void UpdateState(StateManager stateManager)
    {
        //throw new System.NotImplementedException();
    }
}
