using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Stats")]
public class EnemyStatsBase : CharacterStatsBase
{
    [Header("Enemy Attributes")]
    public float stoppingDistance;
    public float fadeOutRate;
}
