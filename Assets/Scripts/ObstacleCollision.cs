using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    GameObject _runner;
    public static event Action OnPlayerDeath;

    private void Start()
    {
        _runner = GameObject.FindGameObjectWithTag("Runner");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Runner")
        {
            Debug.Log("GameOver");
            OnPlayerDeath?.Invoke();
            Destroy(gameObject);
            other.gameObject.SetActive(false);
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
