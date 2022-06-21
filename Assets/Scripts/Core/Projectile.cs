using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 _startingPoint = Vector3.zero;

    [HideInInspector] public float range;
    [HideInInspector] public float damage;
    [HideInInspector] public LayerMask targetMask;

    // Start is called before the first frame update
    void Start()
    {
        _startingPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOnMaxDistanceTraveled();
    }

    private void DestroyOnMaxDistanceTraveled()
    {
        if (Vector3.Distance(transform.position, _startingPoint) > range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((targetMask.value & 1 << other.gameObject.layer) != 0)
        {
            if (other.gameObject.TryGetComponent(out Damageable damageable))
            {
                damageable.InflictDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
