using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IBaseState
{
    public void EnterState(StateManager stateManager)
    {
    }

    public void ExitState(StateManager stateManager)
    {
        stateManager.animator.SetBool(stateManager.RunHash, false);
        stateManager.audioManager.StopSound("footstep");
    }

    public void UpdateState(StateManager stateManager)
    {
        Move(stateManager);
    }

    private void Move(StateManager stateManager)
    {
        if (stateManager.enabled)
        {
            stateManager.SwitchState(stateManager.IdleState);
        }
    }
}
