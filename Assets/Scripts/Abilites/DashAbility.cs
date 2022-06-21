using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ability/Dash")]
public class DashAbility : AbilityBase
{
    public float speed;

    public override void Activate(GameObject parent)
    {
        Animator animator = parent.GetComponent<Animator>();
        Transform transform = parent.GetComponent<Transform>();

        animator.SetTrigger("Dash");
        transform.DOMove(transform.position + (transform.forward * 5), .2f);

        //base.Activate(parent);
    }
}
