
using UnityEngine;

/// <summary>
/// A class that spawns ground tiles to create an endless path
/// </summary>
public class GroundSpawner : MonoBehaviour
{
    public GameObject _groundTile;
    Vector3 _nextSpawnPos;

    /// <summary>
    /// Start spawns the first 15 ground tiles that create the track
    /// </summary>
    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject _temp = Instantiate(_groundTile, _nextSpawnPos, Quaternion.identity);
            _nextSpawnPos = _temp.transform.GetChild(1).transform.position;
        }
    }

    /// <summary>
    /// Spawns the ground tiles that create the track and stores the position of the next spawn location
    /// </summary>
    public void SpawnTile()
    {
        GameObject _temp = Instantiate(_groundTile, _nextSpawnPos, Quaternion.identity);
        _nextSpawnPos = _temp.transform.GetChild(1).transform.position;
    }
}
