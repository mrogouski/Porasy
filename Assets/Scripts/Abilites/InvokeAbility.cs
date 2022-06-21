using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Invoke")]
public class InvokeAbility : AbilityBase
{
    public int quantity;
    public GameObject minionPrefab;

    public override void Activate(GameObject parent)
    {
        var animator = parent.GetComponent<Animator>();
        animator.SetTrigger("Invoke");
        var position = Random.insideUnitSphere + parent.transform.position;
        position *= 2;
        position.y = 0;
        var minion = Instantiate(minionPrefab, position, parent.transform.rotation);

        base.Activate(parent);
    }
}
