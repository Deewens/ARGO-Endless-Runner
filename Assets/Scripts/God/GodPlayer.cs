using System;
using System.Collections;
using UnityEngine;

public class GodPlayer : MonoBehaviour
{
    [SerializeField] private bool isAI = true;
    [Header("Delay between attacks (in seconds")]
    [SerializeField] private float attackCooldownTime = 5f;

    private bool _canAttack = true;

    private AIGod _ai;
    private GodAttack _activeAttack;

    private void Start()
    {
        _ai = GetComponent<AIGod>();
    }

    private void Update()
    {
        if (isAI && _canAttack)
        {
            Attack();
        }
    }

    /// <summary>
    /// Performs the attack and start the cooldown.
    /// </summary>
    private void Attack()
    {
        if (!_canAttack)
            return;
        
        if (isAI)
        {
            _activeAttack = _ai.GetRandomAttack();
        }
        
        _activeAttack.Attack();
        StartCoroutine(StartAttackCooldown());
    }

    /// <summary>
    /// Starts the cooldown that deactivate any attack. The player can't attack during this time.
    /// </summary>
    private IEnumerator StartAttackCooldown()
    {
        _canAttack = false;
        _activeAttack = null;
        yield return new WaitForSeconds(attackCooldownTime);
        _canAttack = true;
    }
}