using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
public class WeaponHandler : MonoBehaviour
{
    private CapsuleCollider _collider;

    public WeaponBase weapon;
    public EquipmentSlotType slotType;
    
    [HideInInspector] public LayerMask targetMask;

    private void Awake()
    {
        _collider = GetComponent<CapsuleCollider>();
        _collider.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if ((targetMask.value & 1 << other.gameObject.layer) != 0)
        {
            if (other.gameObject.TryGetComponent(out Damageable damageable))
            {
                damageable.InflictDamage(weapon.Damage);
            }
        }
    }

    public void EnableCollisions()
    {
        if (_collider != null)
        {
            _collider.enabled = true;
        }
    }

    public void DisableCollisions()
    {
        if (_collider != null)
        {
            _collider.enabled = false;
        }
    }
}
