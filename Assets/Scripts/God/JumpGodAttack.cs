using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpGodAttack : GodAttack
{
    private GameObject jumpAttack;

    /// <summary>
    /// Sets attack type to Jump and loads jump gameobject prefab
    /// </summary>
    private void Start()
    {
        attackType = GodAttackType.Jump;
        jumpAttack = Resources.Load("God Jump Attack") as GameObject;
    }

    /// <summary>
    /// Spawns the avoid gameobject prefab at given target position
    /// </summary>
    /// <param name="targetPos"></param>
    public override void Attack(Vector3 targetPos)
    {
        targetPos = new Vector3(targetPos.x, 0.5f, targetPos.z);
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        Instantiate(jumpAttack, targetPos, rotation);
    }

    public override void Attack()
    {
        throw new System.NotImplementedException();
    }
}
