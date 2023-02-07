
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject _groundTile;
    Vector3 _nextSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            GameObject _temp = Instantiate(_groundTile, _nextSpawnPos, Quaternion.identity);
            _nextSpawnPos = _temp.transform.GetChild(1).transform.position;
        }
    }

    public void SpawnTile()
    {
        Debug.Log( "This : " + _nextSpawnPos.z);
        GameObject _temp = Instantiate(_groundTile, _nextSpawnPos, Quaternion.identity);
        _nextSpawnPos = _temp.transform.GetChild(1).transform.position;
        //Debug.Log("Next : " + _nextSpawnPos.z);

    }


}
