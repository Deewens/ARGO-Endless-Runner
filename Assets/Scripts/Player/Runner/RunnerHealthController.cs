/*
Olympus Run - A game made as part of the ARGO Project at SETU Carlow
Copyright (C) 2023 Caroline Percy <lineypercy@me.com>, Patrick Donnelly <patrickdonnelly3759@gmail.com>, 
                   Izabela Zelek <C00247865@itcarlow.ie>, Danial Hakim <danialhakim01@gmail.com>, 
                   Adrien Dudon <dudonadrien@gmail.com>

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

using Mirror;
using UnityEngine;

/// <summary>
/// Manage the health of the runner and update the health bar.
/// The health value is synchronised with the server.
/// </summary>
public class RunnerHealthController : NetworkBehaviour
{
    private const int MaxHealth = 100;
    private const int HealthBonus = 20;
    
    [SyncVar(hook = nameof(OnHealthChanged))]
    private int _currentHealth = MaxHealth;

    [SerializeField] private HealthBar _healthBarPrefab;
    private HealthBar _healthBar;

    public override void OnStartLocalPlayer()
    {
        _healthBar = Instantiate(_healthBarPrefab);
        _healthBar.SetMaxHealth(MaxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (!isLocalPlayer) return;
        
        CmdTakeDamage(damage);
    }

    /// <summary>
    /// Invoked on Server. Sets the health of the player.
    /// </summary>
    /// <param name="health"></param>
    [Command]
    private void CmdSetHealth(int health)
    {
        _currentHealth = health;
    }
    
    /// <summary>
    /// Invoked on Server. Adds health to the player.
    /// </summary>
    /// <param name="health"></param>
    [Command]
    private void CmdTakeHealth(int health)
    {
        _currentHealth += health;
    }

    /// <summary>
    /// Invoked on Server. Removes health from the player.
    /// </summary>
    /// <param name="damage"></param>
    [Command]
    private void CmdTakeDamage(int damage)
    {
        _currentHealth -= damage;
    }
    
    /// <summary>
    /// Do something on the client whenever health is changed on server
    /// </summary>
    /// <param name="oldHealth"></param>
    /// <param name="newHealth"></param>
    private void OnHealthChanged(int oldHealth, int newHealth)
    {
        if (!isLocalPlayer) return;
        
        _healthBar.SetHealth(newHealth);
    }

    /// <summary>
    /// Give the player full health and sync it to the server
    /// </summary>
    public void InstantHeal()
    {
        CmdSetHealth(MaxHealth);
    }

    /// <summary>
    /// Give the player partial health and sync it to the server
    /// </summary>
    public void PartialHeal()
    {
        CmdTakeHealth(HealthBonus);
    }

    public bool IsRunnerDead()
    {
        return _currentHealth == 0;
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }

    public int GetMaxHealth()
    {
        return MaxHealth;
    }
}
