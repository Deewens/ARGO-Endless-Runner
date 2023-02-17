using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// A class to manage the spawning of objects on the bounds of the track
/// </summary>
public class BushSpawner : MonoBehaviour
{
    public List<GameObject> _sideObject;
    Vector3 _nextSpawnPos;

    /// <summary>
    /// Start spawns the first 15 pairs of columns that line the track
    /// </summary>
    void Start()
    {
        _sideObject[0] = Resources.Load("FoilagePrefabs/Bush0") as GameObject;
        _sideObject[1] = Resources.Load("FoilagePrefabs/Bush1") as GameObject;
        _sideObject[2] = Resources.Load("FoilagePrefabs/Bush2") as GameObject;
        _sideObject[3] = Resources.Load("FoilagePrefabs/Bush3") as GameObject;
        _sideObject[4] = Resources.Load("FoilagePrefabs/Bush4") as GameObject;
        _sideObject[5] = Resources.Load("FoilagePrefabs/Bush5") as GameObject;
        _sideObject[6] = Resources.Load("FoilagePrefabs/Bush6") as GameObject;

        // _sideObject[1] = Resources.Load("Prefabs/MuzzleFlash") as GameObject;

        for (int i = 0; i < 15; i++)
        {
            int Rand = Random.Range(0, 7);
            GameObject _temp = Instantiate(_sideObject[Rand], new Vector3(0, 1, _nextSpawnPos.z), Quaternion.identity);
            _nextSpawnPos = _temp.transform.GetChild(1).transform.position;
        }
    }

    /// <summary>
    /// Spawns the colums that line the track and stores the position of the next spawn location
    /// </summary>
    public void SpawnBush()
    {
        int Rand = Random.Range(0, 7);
        GameObject _temp = Instantiate(_sideObject[Rand], new Vector3(0,1,_nextSpawnPos.z), Quaternion.identity);
        _nextSpawnPos = _temp.transform.GetChild(1).transform.position;
    }


}
