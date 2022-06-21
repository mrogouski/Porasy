using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : ScriptableObject
{
    public string abilityName;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent) { }
}
