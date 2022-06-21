using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CapsuleCollider))]
public class AreaDetection : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool, GameObject> OnTargetDetected;
    [SerializeField] private LayerMask _targetLayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((1 << other.gameObject.layer & _targetLayer.value) != 0)
        {
            OnTargetDetected.Invoke(true, other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if ((1 << other.gameObject.layer & _targetLayer.value) != 0)
        {
            OnTargetDetected.Invoke(true, other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((1 << other.gameObject.layer & _targetLayer.value) != 0)
        {
            OnTargetDetected.Invoke(false, other.gameObject);
        }
    }
}
