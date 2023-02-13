using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideGodAttack : GodAttack
{
    private GameObject _avoidThunder;

    /// <summary>
    /// Sets attack type to Avoid and loads avoid gameobject prefab
    /// </summary>
    private void Start()
    {
        attackType = GodAttackType.Slide;
        _avoidThunder = Resources.Load("AvoidThunder") as GameObject;
    }

    /// <summary>
    /// Spawns the avoid gameobject prefab at given target position
    /// </summary>
    /// <param name="targetPos"></param>
    public override void Attack(Vector3 targetPos)
    {
        Quaternion rotation = Quaternion.Euler(90, 90, 0);
        Instantiate(_avoidThunder, targetPos, rotation);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
