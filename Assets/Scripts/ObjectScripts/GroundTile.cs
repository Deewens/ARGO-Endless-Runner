using System.Collections;
using UnityEngine;

/// <summary>
/// A class that defines the ground tiles that create the path,
/// checks for collision to detect when they are off screen
/// </summary>
public class GroundTile : MonoBehaviour
{
    private GroundSpawner _groundSpawner;

    private void Start()
    {
        _groundSpawner = FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Runner"))
        {
            // Checks for collision with the runners offscreen collider
            // to detect when the ground tiles are off screen.
            // Move the the ground tile to continue the endless path
            StartCoroutine(ReplaceTile());
        }
    }

    /// <summary>
    /// Hide the tile after 2 seconds and move it behind the player.
    /// </summary>
    private IEnumerator ReplaceTile()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        _groundSpawner.MoveTile(this);
    }
}