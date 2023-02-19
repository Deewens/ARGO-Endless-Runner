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

using System;
using UnityEngine;

/// <summary>
/// A class that checks for collision with the player.
/// Determines wheter an obstacle has hit the player 
/// or has gone offscreen
/// </summary>
public class ObstacleCollision : MonoBehaviour
{
    GameObject _runner;
    public static event Action OnPlayerDeath;

    int damage = 20;

    /// <summary>
    /// Finds the runner in the scene
    /// </summary>
    private void Start()
    {
        _runner = GameObject.FindGameObjectWithTag("Runner");
    }

    /// <summary>
    /// Checks for collision with the player directly
    /// and an offscreen collider to determine if the player 
    /// avoided or hit an obstacle
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Runner")
        {
            GetComponent<PointOfInterest>().StartHit(other);
            if (!_runner.GetComponent<RunnerHealthController>().IsRunnerDead())
            {
                _runner.GetComponent<RunnerHealthController>().TakeDamage(damage);
            }
            if (_runner.GetComponent<RunnerHealthController>().IsRunnerDead())
            {
                Debug.Log("GameOver");
                OnPlayerDeath?.Invoke();
                other.gameObject.SetActive(false);
            }
            Destroy(gameObject);
        }
        if (other.tag == "BehindPlayer")
        {

            if (_runner != null)
            {
                //Debug.Log("Avoided");
                _runner.GetComponent<MoveForward>()._obstaclesAvoided += 1;
            }
            else
            {
                _runner = GameObject.FindGameObjectWithTag("Runner");
            }
            Destroy(gameObject, 2);
        }
        
    }
}
