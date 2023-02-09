using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform _runner;
    public GameObject _obstacleWide;
    public GameObject _obstacleHigh;
    public GameObject _obstacleSmall;

    Vector3 _offset;
    int _randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - _runner.position;
        StartCoroutine(SpawnObstacle());
    }

    private void LateUpdate()
    {
        Vector3 newPos = _runner.position + _offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
    }

    public IEnumerator SpawnObstacle()
    {
        Debug.Log("Here");
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
            GameObject _temp = Instantiate(_obstacleSmall, new Vector3(_randomXPos,1,newPos.z), Quaternion.identity);
        }
        else if(_randomNumber == 2)
        {
            GameObject _temp = Instantiate(_obstacleHigh, new Vector3(0, 2, newPos.z), Quaternion.identity);

        }
        else if(_randomNumber == 3)
        {
            GameObject _temp = Instantiate(_obstacleWide, new Vector3(0, 1, newPos.z), Quaternion.identity);
        }
        StartCoroutine(SpawnObstacle());
    }
}
