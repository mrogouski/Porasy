using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Ability/Dash")]
public class DashAbility : AbilityBase
{
    private float distance;
    private float time;

    public float speed;

    public override void Activate(GameObject parent)
    {
        Animator animator = parent.GetComponent<Animator>();
        Transform transform = parent.GetComponent<Transform>();
        NavMeshAgent navMeshAgent = parent.GetComponent<NavMeshAgent>();
        NavMeshPath path = new NavMeshPath();

        navMeshAgent.isStopped = true;
        Vector3 targetPosition = transform.position + (transform.forward * 5);
        distance = Vector3.Distance(transform.position, transform.forward * 5);
        time = distance / speed;

        if (navMeshAgent.CalculatePath(targetPosition, path))
        {
            if (path.status == NavMeshPathStatus.PathComplete)
            {
                //animator.enabled = true;

                animator.SetTrigger("Dash");
                transform.DOMove(transform.position + (transform.forward * 5), .2f);
            }
        }

        //base.Activate(parent);
    }

    IEnumerator AnimateAbility()
    { 

        yield return new WaitForSeconds(time);
    }
}
