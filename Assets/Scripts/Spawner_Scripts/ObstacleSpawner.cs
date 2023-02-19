// Coders:
// Caroline Percy
// ...

using System.Collections;
using UnityEngine;

/// <summary>
/// A class that spawns obstacles of different types at random
/// locations on the track.
/// </summary>
public class ObstacleSpawner : MonoBehaviour
{
    private Transform _runner;
    public Transform Runner
    {
        set
        {
            _runner = value;
            _offset = transform.position - _runner.position;
            StartCoroutine(SpawnObstacle());
        }
    }

    [SerializeField] private GameObject obstacleWidePrefab;
    [SerializeField] private GameObject obstacleHighPrefab;
    [SerializeField] private GameObject obstacleSmallPrefab;
    
    [HideInInspector] public int obstaclesPlaced;

    private Vector3 _offset;
    private int _randomNumber;

    /// <summary>
    /// Updates the position of the spawner to always be in front of the player
    /// </summary>
    private void LateUpdate()
    {
        if (_runner == null)
            return;
        
        Vector3 newPos = _runner.position + _offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
    }

    /// <summary>
    /// Coroutine that handles spawning obstacles of three different types
    /// </summary>
    private IEnumerator SpawnObstacle()
    {
        _randomNumber = Random.Range(1, 4);
        yield return new WaitForSeconds(2.0f);
        Vector3 newPos = _runner.position + _offset;
        if (_randomNumber == 1)
        {
            int randomXPos = Random.Range(1, 4);
            if (randomXPos == 1)
                randomXPos = -2;
            else if (randomXPos == 3)
                randomXPos = 0;
            GameObject temp = Instantiate(obstacleSmallPrefab, new Vector3(randomXPos,3.5f,newPos.z), Quaternion.identity);
            obstaclesPlaced += 1;
        }
        else if(_randomNumber == 2)
        {
            GameObject temp = Instantiate(obstacleHighPrefab, new Vector3(0, 2, newPos.z), Quaternion.identity);
            obstaclesPlaced += 1;
        }
        else if(_randomNumber == 3)
        {
            GameObject temp = Instantiate(obstacleWidePrefab, new Vector3(0, 1, newPos.z), Quaternion.identity);
            obstaclesPlaced += 1;
        }
        StartCoroutine(SpawnObstacle());
    }
}
