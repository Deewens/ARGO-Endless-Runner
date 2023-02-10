using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class to manage the spawning of objects on the bounds of the track
/// </summary>
public class SideObjectSpawner : MonoBehaviour
{
    public GameObject _sideObject;
    Vector3 _nextSpawnPos;
    [SerializeField]
    public GameObject _runnerPlayer;




    /// <summary>
    /// Start spawns the first 15 pairs of columns that line the track
    /// </summary>
    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject _temp = Instantiate(_sideObject, _nextSpawnPos, Quaternion.identity);
            _nextSpawnPos = _temp.transform.GetChild(1).transform.position;
        }
    }

    /// <summary>
    /// Spawns the colums that line the track and stores the position of the next spawn location
    /// </summary>
    public void SpawnSideObject()
    {
        GameObject _temp = Instantiate(_sideObject, _nextSpawnPos, Quaternion.identity);
        _nextSpawnPos = _temp.transform.GetChild(1).transform.position;
    }


}
