using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IBaseState
{
    private float _attackCooldown;
    private float _cooldown;
    private float _aimTime;
    private List<GameObject> _minions;

    public void EnterState(StateManager stateManager)
    {
        Debug.Log("Current State: Attack");

        _minions = new List<GameObject>();
        stateManager.navMeshAgent.isStopped = true;
    }

    public void ExitState(StateManager stateManager)
    {
        //throw new System.NotImplementedException();
    }

    public void UpdateState(StateManager stateManager)
    {
        Debug.DrawLine(stateManager.transform.position, stateManager.TargetPosition, Color.red);

        if (stateManager.TargetOnAttackRange)
        {
            stateManager.transform.LookAt(stateManager.TargetPosition);

            if (stateManager.CharacterStats.canInvoke && _minions.Count < stateManager.CharacterStats.maxMinions && _cooldown <= 0)
            {
                stateManager.animator.SetTrigger(stateManager.InvokeHash);
                _cooldown = stateManager.CharacterStats.cooldownTime;
            }
            else
            {
                _cooldown -= Time.deltaTime;
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
        //else if (stateManager.GotHit)
        //{
        //    stateManager.SwitchState(stateManager.HitState);
        //}
        else
        {
            stateManager.SwitchState(stateManager.PursuitState);
        }
    }
}
