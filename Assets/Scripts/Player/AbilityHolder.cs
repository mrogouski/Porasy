using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    private AbilityState state = AbilityState.ready;
    private float cooldownTime;
    private float activeTime;
    //private PlayerInput playerInput;

    public AbilityBase ability;

    enum AbilityState
    {
        ready,
        active,
        cooldown,
    }

    // Start is called before the first frame update
    void Start()
    {
        if (TryGetComponent(out PlayerInput playerInput))
        {
            playerInput.actions["Ability"].performed += context => ActivateAbility();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                state = AbilityState.ready;
                activeTime = ability.activeTime;
                break;
            case AbilityState.active:
                if (activeTime >= 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime >= 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }

    private void ActivateAbility()
    {
        if (state == AbilityState.ready /*&& cooldownTime <= 0 && activeTime <= 0*/)
        {
            ability.Activate(gameObject);
            state = AbilityState.active;
            activeTime = ability.activeTime;
        }
    }
}
