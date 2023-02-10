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
    public virtual void Attack() { }

    /// <summary>
    /// Takes in a position on the lane and spawns attack there
    /// </summary>
    /// <param name="targetPos"></param>
    public virtual void Attack(Vector3 targetPos) { }
}