using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IBaseState
{
    private float _attackCooldown;
    private float _abilityCooldown;

    public void EnterState(StateManager stateManager)
    {
        Debug.Log("Current State: Attack");

        stateManager.navMeshAgent.isStopped = true;
    }

    public void ExitState(StateManager stateManager)
    {
        //throw new System.NotImplementedException();
    }

    public void UpdateState(StateManager stateManager)
    {
        Debug.DrawLine(stateManager.transform.position, stateManager.TargetPosition, Color.red);

        if (stateManager.GotHit)
        {
            stateManager.SwitchState(stateManager.HitState);
        }
        else if (stateManager.IsDead)
        {
            stateManager.SwitchState(stateManager.DeadState);
        }
        else if (stateManager.TargetOnAttackRange)
        {
            stateManager.transform.LookAt(stateManager.TargetPosition);

            if (stateManager.abilityHolder != null && _abilityCooldown <= 0)
            {
                //stateManager.animator.SetTrigger(stateManager.InvokeHash);
                stateManager.abilityHolder.ActivateAbility();
                _abilityCooldown = stateManager.abilityHolder.ability.cooldownTime;
            }
            else
            {
                _abilityCooldown -= Time.deltaTime;
                var weapon = stateManager.equipmentManager.GetMainHandWeapon();
                if (weapon.AttackType == AttackType.Ranged && _attackCooldown <= 0)
                {
                    stateManager.animator.SetTrigger(stateManager.AttackHash);
                    _attackCooldown = weapon.Speed;
                }
                else
                {
                    _attackCooldown -= Time.deltaTime;
                }

                if (weapon.AttackType == AttackType.Melee && _attackCooldown <= 0)
                {
                    stateManager.animator.SetTrigger(stateManager.AttackHash);
                    _attackCooldown = weapon.Speed;
                }
                else
                {
                    _attackCooldown -= Time.deltaTime;
                }
            }
        }
        else
        {
            stateManager.SwitchState(stateManager.PursuitState);
        }
    }
}
