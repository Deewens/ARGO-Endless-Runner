using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidGodAttack : GodAttack
{
    private GameObject avoidLightning;

    /// <summary>
    /// Sets attack type to Avoid and loads avoid gameobject prefab
    /// </summary>
    private void Start()
    {
        attackType = GodAttackType.Avoid;
        avoidLightning = Resources.Load("AvoidLightning") as GameObject;
    }

    /// <summary>
    /// Spawns the avoid gameobject prefab at given target position
    /// </summary>
    /// <param name="targetPos"></param>
    public override void Attack(Vector3 targetPos)
    {
        Instantiate(avoidLightning, targetPos, Quaternion.identity);
        Debug.Log("Spawned");
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}