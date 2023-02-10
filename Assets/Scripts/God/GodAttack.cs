using UnityEngine;

/// <summary>
/// Inherit from this class to create a new attack for the god player.
/// </summary>
public abstract class GodAttack : MonoBehaviour
{
    protected GodAttackType attackType;
    
    /// <summary>
    /// Contains the logic for the attack, depending on the type of the attack.
    /// </summary>
    /// <example>
    /// If the attack is a thunderbolt, the attack method will spawn a thunderbolt prefab.
    /// </example>
    public abstract void Attack();
}