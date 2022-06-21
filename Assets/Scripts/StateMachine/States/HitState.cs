using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : IBaseState
{
    private Color originalColor;
    private float flashingEffectTime = .15f;

    public void EnterState(StateManager stateManager)
    {
        Debug.Log("Current State: Hit");
        stateManager.StartCoroutine(FlashColorOnHit(stateManager));
    }

    public void ExitState(StateManager stateManager)
    {
        stateManager.GotHit = false;
    }

    public void UpdateState(StateManager stateManager)
    {
        if (stateManager.IsDead)
        {
            stateManager.SwitchState(stateManager.DeadState);
        }
        else
        {
            stateManager.SwitchState(stateManager.IdleState);
        }
    }

    private IEnumerator FlashColorOnHit(StateManager stateManager)
    {
        originalColor = stateManager.mainRenderer.materials[stateManager.mainRenderer.materials.Length - 1].color;
        stateManager.mainRenderer.material.color = Color.red;
        yield return new WaitForSeconds(flashingEffectTime);
        stateManager.mainRenderer.material.color = originalColor;
    }
}
