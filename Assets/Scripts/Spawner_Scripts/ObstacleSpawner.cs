// Coders:
// Caroline Percy
// ...

using System.Collections;
using System.Collections.Generic;
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

    public GameObject _obstacleWide;
    public GameObject _obstacleHigh;
    public GameObject _obstacleSmall;
    public int _obstaclesPlaced;

    Vector3 _offset;
    int _randomNumber;

    /// <summary>
    /// Sets up the original offset between the player and the spawner and starts the
    /// spawning coroutine
    /// </summary>
    void Start()
    {
        _obstaclesPlaced = 0;
    }

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
    public IEnumerator SpawnObstacle()
    {
        //Debug.Log("Here");
        _randomNumber = Random.Range(1, 4);
        //_randomNumber = 1;
        yield return new WaitForSeconds(2.0f);
        Vector3 newPos = _runner.position + _offset;
        if (_randomNumber == 1)
        {
            int _randomXPos = Random.Range(1, 4);
            if (_randomXPos == 1)
                _randomXPos = -2;
            else if (_randomXPos == 3)
                _randomXPos = 0;
            GameObject _temp = Instantiate(_obstacleSmall, new Vector3(_randomXPos,3.5f,newPos.z), Quaternion.identity);
            _obstaclesPlaced += 1;
        }
        else if(_randomNumber == 2)
        {
            GameObject _temp = Instantiate(_obstacleHigh, new Vector3(0, 2, newPos.z), Quaternion.identity);
            _obstaclesPlaced += 1;
        }
        else if(_randomNumber == 3)
        {
            GameObject _temp = Instantiate(_obstacleWide, new Vector3(0, 1, newPos.z), Quaternion.identity);
            _obstaclesPlaced += 1;
        }
        StartCoroutine(SpawnObstacle());
    }

    /// <summary>
    /// Spawns a new small obstacle into the scene.
    /// </summary>
    /// <returns>The new small obstacle.</returns>
    public GameObject SpawnSmallObstacle()
    {
        GameObject newObstacle = Instantiate(_obstacleSmall);
        newObstacle.SetActive(true);
        return newObstacle;
    }

    /// <summary>
    /// Spawns a new wide obstacle into the scene.
    /// </summary>
    /// <returns>The new wide obstacle.</returns>
    public GameObject SpawnWideObstacle()
    {
        GameObject newObstacle = Instantiate(_obstacleWide);
        newObstacle.SetActive(true);
        return newObstacle;
    }

    /// <summary>
    /// Spawns a new high obstacle into the scene.
    /// </summary>
    /// <returns>The new high obstacle.</returns>
    public GameObject SpawnHighObstacle()
    {
        GameObject newObstacle = Instantiate(_obstacleHigh);
        newObstacle.SetActive(true);
        return newObstacle;
    }
}
