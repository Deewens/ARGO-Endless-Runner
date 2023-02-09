using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    public static event Action OnPlayerDeath;

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
            Debug.Log("Avoided");
            Destroy(gameObject, 2);
        }
        
    }
}
