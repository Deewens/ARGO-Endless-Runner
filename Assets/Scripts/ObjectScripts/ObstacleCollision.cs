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

using System;
using System.Collections;
using Mirror;
using UnityEngine;

/// <summary>
/// A class that checks for collision with the player.
/// Determines wheter an obstacle has hit the player 
/// or has gone offscreen
/// </summary>
public class ObstacleCollision : MonoBehaviour
{
    private GameObject _runner;
    public static event Action OnPlayerDeath;

    private const int Damage = 20;
    
    [ServerCallback]
    private void Start()
    {
        _runner = GameObject.FindGameObjectWithTag("Runner");
    }

    /// <summary>
    /// Checks for collision with the player directly
    /// and an offscreen collider to determine if the player 
    /// avoided or hit an obstacle
    /// </summary>
    [ServerCallback]
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Runner"))
        {
            GetComponent<PointOfInterest>().StartHit(other);
            if (!_runner.GetComponent<RunnerHealthController>().IsRunnerDead())
            {
                _runner.GetComponent<RunnerHealthController>().TakeDamage(Damage);
            }
            if (_runner.GetComponent<RunnerHealthController>().IsRunnerDead())
            {
                OnPlayerDeath?.Invoke();
                other.gameObject.SetActive(false);
            }
            NetworkServer.Destroy(gameObject);
        }
        
        if (other.CompareTag("BehindPlayer"))
        {

            if (_runner != null)
            {
                _runner.GetComponent<MoveForward>().ObstaclesAvoided += 1;
            }
            else
            {
                _runner = GameObject.FindGameObjectWithTag("Runner");
            }

            StartCoroutine(ServerDestroyAfterTime(2f));
        }
    }

    [Server]
    private IEnumerator ServerDestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        NetworkServer.Destroy(gameObject);
    }
}
