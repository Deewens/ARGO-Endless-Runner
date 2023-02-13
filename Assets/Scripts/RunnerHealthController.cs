using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RunnerHealthController : MonoBehaviour
{
    private int _maxHealth = 100;
    private int _currentHealth;
    private int _healthBonus = 20;

    [SerializeField] private HealthBar healthBarPrefab;
    private HealthBar _healthBar;
    
    void Start()
    {
        _healthBar = Instantiate(healthBarPrefab);
        
        _currentHealth = _maxHealth;
        _healthBar.SetMaxHealth(_maxHealth);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        _healthBar.SetHealth(_currentHealth);
    }

    public void InstaHeal()
    {
        _currentHealth = _maxHealth;

        _healthBar.SetMaxHealth(_maxHealth);
    }

    public void PartialHeal()
    {
        _currentHealth += _healthBonus;

        _healthBar.SetHealth(_maxHealth);
    }

    public bool IsRunnerDead()
    {
        return _currentHealth == 0;
    }

    public int GetHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return _maxHealth;
    }
}
