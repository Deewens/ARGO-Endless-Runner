//Author : Izabela Zelek, February 2023
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Setting and spawning an avoidable obstacle at given position
/// </summary>
public class AvoidGodAttack : GodAttack
{
    private GameObject _avoidLightning;

    /// <summary>
    /// Sets attack type to Avoid and loads avoid gameobject prefab
    /// </summary>
    private void Start()
    {
        attackType = GodAttackType.Avoid;
        _avoidLightning = Resources.Load("AvoidLightning") as GameObject;
    }

    /// <summary>
    /// Spawns the avoid gameobject prefab at given target position
    /// </summary>
    /// <param name="targetPos"></param>
    public override void Attack(Vector3 targetPos)
    {
        Instantiate(_avoidLightning, targetPos, Quaternion.identity);
    }
}