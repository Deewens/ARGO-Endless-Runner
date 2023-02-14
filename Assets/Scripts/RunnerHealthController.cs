using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunnerHealthController : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _currentHealth;
    private int _healthBonus = 20;

    public HealthBar healthBar;
    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
        healthBar.SetMaxHealth(_maxHealth);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        healthBar.SetHealth(_currentHealth);
    }

    public void InstaHeal()
    {
        _currentHealth = _maxHealth;

        healthBar.SetMaxHealth(_maxHealth);
    }

    public void PartialHeal()
    {
        _currentHealth += _healthBonus;

        healthBar.SetHealth(_maxHealth);
    }

    public bool IsRunnerDead()
    {
        return _currentHealth == 0;
    }
}
