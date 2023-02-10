using UnityEngine;

/// <summary>
/// Just an example script to show how to create a new attack.
/// </summary>
public class DummyGodAttack : GodAttack
{
    /// <summary>
    /// Don't forget to set the attack type in the Start method.
    /// </summary>
    private void Start()
    {
        attackType = GodAttackType.Dummy;
    }

    /// <summary>
    /// Just an example method to show how to create a new attack with target position. You should put the attack logic here in real code.
    /// </summary>
    public override void Attack(Vector3 targetPos)
    {
        Debug.Log("God attack with 'target pos'!");
    }

    /// <summary>
    /// Just an example method to show how to create a new attack. You should put the attack logic here in real code.
    /// </summary>
    public override void Attack()
    {
        Debug.Log("God attack with 'Dummy'!");
    }
}