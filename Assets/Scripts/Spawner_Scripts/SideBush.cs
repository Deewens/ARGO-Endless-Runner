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
    private void Start()
    {
        _bushSpawner = FindObjectOfType<BushSpawner>();
    }

    /// <summary>
    /// Checks for collision with the collider behind the player to
    /// check if off screen. Spawns a new side object and destroys the old one
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Runner"))
        {
            StartCoroutine(ReplaceAndMove());
        }
    }

    private IEnumerator ReplaceAndMove()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        _bushSpawner.PlaceRandomBush();
    }
}
