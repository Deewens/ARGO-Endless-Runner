using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGodAttackList : MonoBehaviour
{
    [SerializeField] private List<GodAttack> attackList = new List<GodAttack>();

    /// <summary>
    /// Returns the requested attack
    /// </summary>
    /// <param name="chosenAttack"></param>
    /// <returns></returns>
    public GodAttack GetAttack(int chosenAttack)
    {
        return attackList[chosenAttack];
    }
}