/*
Olympus Run - A game made as part of the ARGO Project at SETU Carlow
Copyright (C) 2023 Caroline Percy <lineypercy@me.com>, Patrick Donnelly <patrickdonnelly3759@gmail.com>, Izabela Zelek <C00247865@itcarlow.ie>, Danial-hakim <danialhakim01@gmail.com>, Adrien Dudon <dudonadrien@gmail.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using System.Collections;
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
        _runner.GetComponent<MoveForward>().SetSpeed(+2);
    }
    public void SpeedDown()
    {
        _runner.GetComponent<MoveForward>().SetSpeed(-2);
    }
}
