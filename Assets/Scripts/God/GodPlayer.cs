using System;
using System.Collections;
using UnityEngine;
/// <summary>
/// A class managing the God Player attacks
/// </summary>
public class GodPlayer : MonoBehaviour
{
    [SerializeField] private bool isAI = true;
    [Header("Delay between attacks (in seconds")]
    [SerializeField] private float attackCooldownTime = 5f;
    private bool _canAttack = true;
    private AIGod _ai;
    private GodAttack _activeAttack;

    private PlayerGodAttackList _playerAttack;
    private Vector3 _attackPos = new Vector3();
    private UIGameManager _uIGameManager;

    private void Start()
    {
        _ai = GetComponent<AIGod>();
        _playerAttack = GameObject.Find("UIGameManager").GetComponent<PlayerGodAttackList>();
        _uIGameManager = GameObject.Find("UIGameManager").GetComponent<UIGameManager>();
    }

    /// <summary>
    /// if is an AI, calls Attack
    /// If not an AI and detects touch, grabs and converts the position to world space
    /// </summary>
    private void Update()
    {
        if (isAI && _canAttack)
        {
            Attack();
        }

        if (!isAI && _canAttack)
        {
            if (Input.GetMouseButtonDown(0) && _uIGameManager.GetChosenAttack() != -1)
            {
                _activeAttack = _playerAttack.GetAttack(_uIGameManager.GetChosenAttack());
                Vector3 pos = Input.mousePosition;
                pos.z = Camera.main.nearClipPlane + 30;
                pos = Camera.main.ScreenToWorldPoint(pos);
                if(_uIGameManager.GetChosenAttack() == 1)
                {
                    pos.y = 3;
                }
                else
                {
                    pos.y = 2;
                }
                _attackPos = pos;
                PlayerAttack();
            }
            else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && _uIGameManager.GetChosenAttack() != -1)
            {
                _activeAttack = _playerAttack.GetAttack(_uIGameManager.GetChosenAttack());
                Vector3 pos = Input.GetTouch(0).position;
                pos.z = Camera.main.nearClipPlane + 30;
                pos = Camera.main.ScreenToWorldPoint(pos);
                pos.y = 2;
                _attackPos = pos;
                PlayerAttack();
            }
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

    /// <summary>
    /// Performs the player attack and start the cooldown.
    /// </summary>
    private void PlayerAttack()
    {
        if (!_canAttack)
            return;

        if (!isAI)
        {
            _activeAttack.Attack(_attackPos);
            StartCoroutine(StartAttackCooldown());
        }
    }
}