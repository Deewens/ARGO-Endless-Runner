using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to manage the spawning of objects on the bounds of the track
/// </summary>
public class SideBush : MonoBehaviour
{
    private BushSpawner _bushSpawner;

    /// <summary>
    /// Finds the spawner of this object in the scene
    /// </summary>
    void Start()
    {
        _bushSpawner = GameObject.FindObjectOfType<BushSpawner>();
    }

    /// <summary>
    /// Checks for collision with the collider behind the player to
    /// check if off screen. Spawns a new side object and destroys the old one
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Runner")
        {
            _bushSpawner.SpawnBush();
            Destroy(gameObject, 2);
        }
    }
}
