/*
Olympus Run - A game made as part of the ARGO Project at SETU Carlow
Copyright (C) 2023 Caroline Percy <lineypercy@me.com>, Patrick Donnelly <patrickdonnelly3759@gmail.com>, Izabela Zelek <C00247865@itcarlow.ie>, Danial-hakim <danialhakim01@gmail.com>, Adrien Dudon <dudonadrien@gmail.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using UnityEngine;

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
