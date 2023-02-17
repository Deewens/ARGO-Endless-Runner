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
    public Transform Runner
    {
        set
        {
            _runner = value;
            StartCoroutine(SpawnPickup());
        }
    }

    private GameObject _coin;
    private GameObject _pom;
    private GameObject _apple;
    private GameObject _speedUp;
    private GameObject _speedDown;
    private GameObject _maxHealth;
    private GameObject _partHealth;

    private float _timer = 0.0f;
    private void Start()
    {
        _offset = new Vector3(0,0,40);
        _coin = Resources.Load("Pickups/Coin") as GameObject;
        _pom = Resources.Load("Pickups/Pomegranate") as GameObject;
        _apple = Resources.Load("Pickups/Apple") as GameObject;
        _speedUp = Resources.Load("Pickups/SpeedUpPotion") as GameObject;
        _speedDown = Resources.Load("Pickups/SpeedDownPotion") as GameObject;
        _maxHealth = Resources.Load("Pickups/MaxHealthPotion") as GameObject;
        _partHealth = Resources.Load("Pickups/PartHealthPotion") as GameObject;
    }

    private void Update()
    {

        if (Time.timeScale != 1.0f && _timer > 0.0f)
        {
            _timer -= Time.deltaTime;
        }
        if (Time.timeScale != 1.0f && _timer <= 0.0f)
        {
            Time.timeScale = 1.0f;
        }
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
    /// Coroutine that handles spawning pickups
    /// </summary>
    public IEnumerator SpawnPickup()
    {
        Debug.Log("Spawning");
        while (_runner.GetChild(0).gameObject.activeSelf)
        {
            yield return new WaitForSeconds(1.0f);

            int pickOrPower = Random.Range(1, 4);
            int randX = Random.Range(-2, 2);
            if (randX < 0)
            {
                randX = -2;
            }
            else if (randX > 0)
            {
                randX = 2;
            }
            Vector3 newPos = _runner.position + _offset;
            if(pickOrPower == 1 && Time.timeScale == 1.0f)
            {
                int randomNumber = Random.Range(1, 5);

                switch (randomNumber)
                {
                    case 1:
                        Instantiate(_speedUp, new Vector3(randX, 2, newPos.z), Quaternion.identity);
                        break;
                    case 2:
                        Instantiate(_speedDown, new Vector3(randX, 2, newPos.z), Quaternion.identity);
                        break;
                    case 3:
                        Instantiate(_maxHealth, new Vector3(randX, 2, newPos.z), Quaternion.identity);
                        break;
                    case 4:
                        Instantiate(_partHealth, new Vector3(randX, 2, newPos.z), Quaternion.identity);
                        break;
                }
            }
            else
            {
                int randomNumber = Random.Range(1, 4);

                switch (randomNumber)
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

    public void SpeedUp()
    {
        //Time.timeScale = 2.0f;
        //_timer = 10.0f;
    }
    public void SpeedDown()
    {
        //Time.timeScale = 0.5f;
        //_timer = 5.0f;
    }
}
