using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public Dictionary<EquipmentSlotType, WeaponHandler> equipmentSlots;
    public LayerMask targetLayer;

    private void Awake()
    {
        equipmentSlots = new Dictionary<EquipmentSlotType, WeaponHandler>();

        var weapons = GetComponentsInChildren(typeof(WeaponHandler), true);

        foreach (WeaponHandler weapon in weapons)
        {
            weapon.targetMask = targetLayer;

            if (equipmentSlots.ContainsKey(weapon.slotType))
            {
                equipmentSlots[weapon.slotType] = weapon;
            }
            else
            {
                equipmentSlots.Add(weapon.slotType, weapon);
            }
        }
    }

    public void EnableDamageCollider(EquipmentSlotType slotType)
    {
        if (equipmentSlots.ContainsKey(slotType))
        {
            if (equipmentSlots[EquipmentSlotType.RightHand].weapon.AttackType == AttackType.Melee)
            {
                var weaponHandler = equipmentSlots[slotType].GetComponentInChildren<WeaponHandler>();
                weaponHandler.EnableCollisions();
            }
        }
    }

    public void DisableDamageCollider(EquipmentSlotType slotType)
    {
        if (equipmentSlots.ContainsKey(slotType))
        {
            if (equipmentSlots[EquipmentSlotType.RightHand].weapon.AttackType != AttackType.Ranged)
            {
                var weaponHandler = equipmentSlots[slotType].GetComponentInChildren<WeaponHandler>();
                weaponHandler.DisableCollisions();
            }
        }
    }

    public WeaponBase GetMainHandWeapon()
    {
        if (equipmentSlots.ContainsKey(EquipmentSlotType.RightHand))
        {
            return equipmentSlots[EquipmentSlotType.RightHand].weapon;
        }
        else
        {
            return null;
        }
    }

    public WeaponBase GetOffHandWeapon()
    {
        if (equipmentSlots.ContainsKey(EquipmentSlotType.RightHand))
        {
            return equipmentSlots[EquipmentSlotType.RightHand].weapon;
        }
        else
        {
            return null;
        }
    }

    //public List<WeaponBase> GetEquippedWeapons()
    //{
    //    List<WeaponBase> weapons = new List<WeaponBase>();

    //    if (equipmentSlots.ContainsKey(SlotType.RightHand))
    //    {
    //        var weaponHandler = equipmentSlots[SlotType.RightHand].EquippedItem.GetComponentInChildren<WeaponHandler>();
    //        weapons.Add(weaponHandler.Weapon);
    //    }
    //    else if (equipmentSlots.ContainsKey(SlotType.LeftHand))
    //    {
    //        var weaponHandler = equipmentSlots[SlotType.LeftHand].EquippedItem.GetComponentInChildren<WeaponHandler>();
    //        weapons.Add(weaponHandler.Weapon);
    //    }

    //    return weapons;
    //}
}
