using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBaseState
{
    public void EnterState(StateManager stateManager);

    public void UpdateState(StateManager stateManager);

    public void ExitState(StateManager stateManager);
}
