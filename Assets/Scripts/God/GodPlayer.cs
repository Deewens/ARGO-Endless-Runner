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
    int chosenAttack = -1;

    private PlayerGodAttackList _playerAttack;
    private Vector3 _attackPos = new Vector3();

    public float distance = 10f;

    public LayerMask mask;

    private void Start()
    {
        _ai = GetComponent<AIGod>();
        _playerAttack = GameObject.Find("God Canvas").GetComponent<PlayerGodAttackList>();
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
            if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && chosenAttack != -1)
            {
                if (PlacementAllow())
                {
                    _activeAttack = _playerAttack.GetAttack(chosenAttack);
                    Vector3 pos = Input.mousePosition;
                    pos.z = Camera.main.nearClipPlane + 30;
                    pos = Camera.main.ScreenToWorldPoint(pos);
                    if (chosenAttack == 1)
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
                else
                {
                    Debug.Log("pick another place");
                }
            }
        }
    }

    public void ChooseAttack(int t_attack)
    {
        _activeAttack = _playerAttack.GetAttack(t_attack);
        chosenAttack = t_attack;
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

    private bool PlacementAllow()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        // Check if the raycast hits a specific GameObject
        if (Physics.Raycast(ray, out hitInfo,Mathf.Infinity,mask))
        {
            Debug.Log(hitInfo.collider.gameObject.tag);
            GameObject hitObject = hitInfo.collider.gameObject;
            if (hitObject.tag == "Ground")
            {
                return true;
            }
        }
        return false;
    }
}