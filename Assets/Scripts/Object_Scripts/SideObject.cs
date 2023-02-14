using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that defines the side objects that line the path,
/// checks for collision to detect when they are off screen
/// </summary>
public class SideObject : MonoBehaviour
{
    private SideObjectSpawner _sideObjectSpawner;

    /// <summary>
    /// Finds the spawner of this object in the scene
    /// </summary>
    void Start()
    {
        _sideObjectSpawner = GameObject.FindObjectOfType<SideObjectSpawner>();
    }

    /// <summary>
    /// Checks for collision with the collider behind the player to
    /// check if off screen. Spawns a new side object and destroys the old one
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Runner")
        {
            _sideObjectSpawner.SpawnSideObject();
            Destroy(gameObject, 2);
        }
    }
}
