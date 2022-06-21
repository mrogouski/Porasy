using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Weapon")]
public class WeaponBase : ItemBase
{
    [Header("Weapon Attributes")]
    public WeaponType WeaponType;
    public AttackType AttackType;
    public float Damage;
    public float Speed;
    public float Range;
    public GameObject projectilePrefab;
}
