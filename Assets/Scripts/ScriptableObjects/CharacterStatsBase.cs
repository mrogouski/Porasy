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
}
