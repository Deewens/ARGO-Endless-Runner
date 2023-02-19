
using UnityEngine;

/// <summary>
/// A class that spawns ground tiles to create an endless path
/// </summary>
public class GroundSpawner : MonoBehaviour
{
    [SerializeField] private GameObject groundTilePrefab;
    private Vector3 _nextSpawnPos;

    /// <summary>
    /// Start spawns the first 15 ground tiles that create the track
    /// </summary>
    private void Start()
    {
        for (var i = 0; i < 15; i++)
        {
            var groundTile = Instantiate(groundTilePrefab, _nextSpawnPos, Quaternion.identity);
            _nextSpawnPos = groundTile.transform.GetChild(1).transform.position;
        }
    }
    
    /// <summary>
    /// Move the tile that has gone off screen to the end of the track
    /// </summary>
    /// <param name="tile">Tile to be moved</param>
    public void MoveTile(GroundTile tile)
    {
        tile.transform.position = _nextSpawnPos;
        _nextSpawnPos = tile.transform.GetChild(1).transform.position;
        tile.gameObject.SetActive(true);
    }
}
