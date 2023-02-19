using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// A class to manage the spawning of objects on the bounds of the track
/// </summary>
public class BushSpawner : MonoBehaviour
{
    private readonly List<GameObject> _sideObjectPrefabs = new();
    private Vector3 _nextSpawnPos;

    private readonly List<SideBush> _sideBushPool = new();

    /// <summary>
    /// Start spawns the first 15 pairs of columns that line the track
    /// </summary>
    private void Start()
    {
        _sideObjectPrefabs.Add(Resources.Load("FoilagePrefabs/Bush0") as GameObject);
        _sideObjectPrefabs.Add(Resources.Load("FoilagePrefabs/Bush1") as GameObject);
        _sideObjectPrefabs.Add(Resources.Load("FoilagePrefabs/Bush2") as GameObject);
        _sideObjectPrefabs.Add(Resources.Load("FoilagePrefabs/Bush3") as GameObject);
        _sideObjectPrefabs.Add(Resources.Load("FoilagePrefabs/Bush4") as GameObject);
        _sideObjectPrefabs.Add(Resources.Load("FoilagePrefabs/Bush5") as GameObject);
        _sideObjectPrefabs.Add(Resources.Load("FoilagePrefabs/Bush6") as GameObject);

        // Instantiate every bush twice to have a pool of 14 bushes
        for (var i = 0; i < 2; i++)
        {
            foreach (var sideObjectPrefab in _sideObjectPrefabs)
            {
                var bushGO = Instantiate(sideObjectPrefab);
                bushGO.SetActive(false);
                _sideBushPool.Add(bushGO.GetComponent<SideBush>());
            }
        }
        
        // Active randomly selected 14 bushes from the object pool
        for (var i = 0; i < 15; i++)
        {
            var inactiveBush = _sideBushPool.Where(bush => !bush.gameObject.activeSelf).ToList();
        
            var rand = Random.Range(0, inactiveBush.Count);
            inactiveBush[rand].transform.position = new Vector3(0, 1, _nextSpawnPos.z);
            inactiveBush[rand].gameObject.SetActive(true);
            _nextSpawnPos = inactiveBush[rand].transform.GetChild(1).transform.position;
        }
    }

    /// <summary>
    /// Randomly select a bush from the pool and place it at the end of the track
    /// </summary>
    public void PlaceRandomBush()
    {
        var inactiveBush = _sideBushPool.Where(bush => !bush.gameObject.activeSelf).ToList();
        
        var rand = Random.Range(0, inactiveBush.Count);
        inactiveBush[rand].transform.position = new Vector3(0, 1, _nextSpawnPos.z);
        inactiveBush[rand].gameObject.SetActive(true);
        _nextSpawnPos = inactiveBush[rand].transform.GetChild(1).transform.position;
    }
}
