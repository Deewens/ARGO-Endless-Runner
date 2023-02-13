using UnityEngine;

/// <summary>
/// A class that defines the ground tiles that create the path,
/// checks for collision to detect when they are off screen
/// </summary>
public class GroundTile : MonoBehaviour
{
    private GroundSpawner _groundSpawner;

    /// <summary>
    /// Finds the spawner of this object in the scene
    /// </summary>
    void Start()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    /// <summary>
    /// Checks for collision with the runners offscreen collider
    /// to detect when the ground tiles are off screen.
    /// Respawns a new ground tile to continue the endless path
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Runner")
        {
            _groundSpawner.SpawnTile();
            Destroy(gameObject, 2);
        }
    }
}
