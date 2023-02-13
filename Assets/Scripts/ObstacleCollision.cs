using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            //if(!_runner.GetComponent<RunnerHealthController>().IsRunnerDead())
            //{
            //    _runner.GetComponent<RunnerHealthController>().TakeDamage(damage);
            //}
            //if (_runner.GetComponent<RunnerHealthController>().IsRunnerDead())
            //{
            //    Debug.Log("GameOver");
            //    OnPlayerDeath?.Invoke();
            //    other.gameObject.SetActive(false);
            //}
            Destroy(gameObject);
        }
        if (other.tag == "BehindPlayer")
        {

            if (_runner != null)
            {
                Debug.Log("Avoided");
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
