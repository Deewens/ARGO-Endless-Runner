
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject _groundTile;
    Vector3 _nextSpawnPos;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        GameObject _temp = Instantiate(_groundTile, _nextSpawnPos, Quaternion.identity);
        _nextSpawnPos = _temp.transform.GetChild(1).transform.position;
    }


}
