using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PursuitState : IBaseState
{
    public void EnterState(StateManager stateManager)
    {
        Debug.Log("Current State: Pursuit");
        //_animator.SetTrigger(stateManager.AlertHash);

    }

    public void ExitState(StateManager stateManager)
    {
        stateManager.animator.SetBool(stateManager.RunHash, false);
        stateManager.navMeshAgent.isStopped = true;
    }

    public void UpdateState(StateManager stateManager)
    {
        if (stateManager.GotHit)
        {
            stateManager.SwitchState(stateManager.HitState);
        }
        else if (stateManager.IsDead)
        {
            stateManager.SwitchState(stateManager.DeadState);
        }
        if (stateManager.TargetDetected)
        {
            if (Vector3.Distance(stateManager.transform.position, stateManager.TargetPosition) > stateManager.CharacterStats.stoppingDistance)
            {
                Debug.DrawLine(stateManager.transform.position, stateManager.TargetPosition, Color.green);
                stateManager.gameObject.transform.LookAt(stateManager.TargetPosition);
                stateManager.navMeshAgent.stoppingDistance = stateManager.CharacterStats.stoppingDistance;
                stateManager.navMeshAgent.speed = stateManager.CharacterStats.movementSpeed;
                stateManager.navMeshAgent.SetDestination(stateManager.TargetPosition);
                stateManager.navMeshAgent.isStopped = false;
                stateManager.animator.SetBool(stateManager.RunHash, true);
            }
            else if (Vector3.Distance(stateManager.transform.position, stateManager.TargetPosition) <= stateManager.CharacterStats.stoppingDistance)
            {
                stateManager.SwitchState(stateManager.AttackState);
            }
        }
        else
        {
            stateManager.SwitchState(stateManager.IdleState);
        }
    }
}
