using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the spawning of pickups
/// </summary>
public class PickupController : MonoBehaviour
{
    private Vector3 _offset = new Vector3();
    private Transform _runner;
    private GameObject _coin;
    private GameObject _pom;
    private GameObject _apple;
    private void Start()
    {
        _offset = new Vector3(0,0,40);
        _runner = GameObject.FindGameObjectWithTag("Runner").transform;
        _coin = Resources.Load("Coin") as GameObject;
        _pom = Resources.Load("Pomegranate") as GameObject;
        _apple = Resources.Load("Apple") as GameObject;
        StartCoroutine(SpawnPickup());
    }

    /// <summary>
    /// Updates the position of the spawner to always be in front of the player
    /// </summary>
    private void LateUpdate()
    {
        Vector3 newPos = _runner.position + _offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
    }

    /// <summary>
    /// Coroutine that handles spawning pickups
    /// </summary>
    public IEnumerator SpawnPickup()
    {
        while(_runner.GetChild(0).gameObject.activeSelf)
        { 
            int _randomNumber = Random.Range(1,4);

            yield return new WaitForSeconds(1.0f);
            int randX = Random.Range(-2, 2);
            if(randX < 0)
            {
                randX = -2;
            }
            else if(randX > 0)
            {
                randX = 2;
            }
            Vector3 newPos = _runner.position + _offset;

            switch(_randomNumber)
            {
                case 1:
                    Instantiate(_coin, new Vector3(randX, 2, newPos.z), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(_pom, new Vector3(randX, 2, newPos.z), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(_apple, new Vector3(randX, 2, newPos.z), Quaternion.identity);
                    break;
            }
        }
    }
}
