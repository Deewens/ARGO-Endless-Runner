using System;
using System.Collections.Generic;
using UnityEngine;

public class AIGod : MonoBehaviour
{
    [SerializeField] private List<GodAttack> attackList = new List<GodAttack>();
    
    /// <summary>
    /// Simply returns a random attack from the list.
    /// </summary>
    /// <remarks>
    /// This method will be replaced by a proper AI logic later on (probably using fuzzy logic or something like that).
    /// </remarks>
    /// <returns>The selected attack</returns>
    public GodAttack GetRandomAttack()
    {
        int rand = UnityEngine.Random.Range(0, attackList.Count);
        return attackList[rand];
    }
}