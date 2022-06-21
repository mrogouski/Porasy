using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/Stats")]
public class CharacterStatsBase : ScriptableObject
{
    [Header("Base Attributes")]
    public CharacterType characterType;
    public float movementSpeed;
    public float maxHealth;

    [Header("Invokers Attributes")]
    public bool canInvoke;
    public float cooldownTime;
    public GameObject minionPrefab;
    public int maxMinions;
}
