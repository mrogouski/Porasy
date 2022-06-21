using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IBaseState
{
    public void EnterState(StateManager stateManager)
    {
        Debug.Log("Current State: Idle");
    }

    public void ExitState(StateManager stateManager)
    {
    }

    public void UpdateState(StateManager stateManager)
    {
        if (stateManager.TargetDetected)
        {
            stateManager.SwitchState(stateManager.PursuitState);
        }
        else if (stateManager.GotHit)
        {
            stateManager.SwitchState(stateManager.HitState);
        }
        else
        {
            if (Vector3.Distance(stateManager.SpawnPoint, stateManager.transform.position) > 1)
            {
                stateManager.navMeshAgent.SetDestination(stateManager.SpawnPoint);
                stateManager.navMeshAgent.stoppingDistance = 0;
                stateManager.navMeshAgent.isStopped = false;
                stateManager.animator.SetBool(stateManager.RunHash, false);
            }
            else
            {
                stateManager.navMeshAgent.isStopped = true;
            }
        }

        //if (SearchForTarget(stateManager, 10f, out Vector3 targetPosition))
        //{
        //    stateManager.TargetPosition = targetPosition;
        //    stateManager.SwitchState(stateManager.PursuitState);
        //}
    }

    //private bool SearchForTarget(StateManager stateManager, float searchDistance, out Vector3 targetPosition)
    //{
    //    var colliders = Physics.OverlapSphere(stateManager.transform.position, searchDistance, LayerMask.GetMask("Player"));
    //    foreach (var item in colliders)
    //    {
    //        targetPosition = item.transform.position;
    //        return true;
    //    }

    //    targetPosition = Vector3.zero;
    //    return false;
    //}
}
