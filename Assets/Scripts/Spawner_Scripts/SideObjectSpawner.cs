using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// A class to manage the spawning of objects on the bounds of the track
/// </summary>
public class SideObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject sideObjectPrefab;
    private Vector3 _nextSpawnPos;

    /// <summary>
    /// Start spawns the first 15 pairs of columns that line the track
    /// </summary>
    private void Start()
    {
        for (var i = 0; i < 15; i++)
        {
            var sideObject = Instantiate(sideObjectPrefab, _nextSpawnPos, Quaternion.identity);
            _nextSpawnPos = sideObject.transform.GetChild(1).transform.position;
        }
    }

    /// <summary>
    /// Spawns the columns that line the track and stores the position of the next spawn location
    /// </summary>
    public void MoveSideObject(SideObject sideObject)
    {
        sideObject.transform.position = _nextSpawnPos;
        _nextSpawnPos = sideObject.transform.GetChild(1).transform.position;
        sideObject.gameObject.SetActive(true);
    }
}
