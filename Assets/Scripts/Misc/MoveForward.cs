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

using Mirror;
using UnityEngine;

/// <summary>
/// A class that moves the player forward and tracks the distance they have travelled
/// </summary>
public class MoveForward : NetworkBehaviour
{
    private double _startTime;
    private double _endTime;
    private double _playTime;
    private int _speed;
    private const int MaxSpeed = 30;
    public Rigidbody _rb;
    private int _distanceTravelled;
    int _startingPos;
    int _increaseWhen;
    public int _obstaclesAvoided;

    /// <summary>
    /// Sets up default values
    /// </summary>
    void Start()
    {
        _endTime = 0;
        _playTime = 0;
        _startTime = System.DateTimeOffset.Now.ToUnixTimeSeconds();
        _increaseWhen = 100;
        _speed = 8;
        _startingPos = (int)transform.position.z;
        _obstaclesAvoided = 0;
    }


    /// <summary>
    /// Calculates the distance travelled by the runner and increases speed based on distance travelled
    /// </summary>
    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        
        _distanceTravelled = (int)transform.position.z - (int)_startingPos;

        if (_distanceTravelled % _increaseWhen == 0 && _distanceTravelled != 0)
        {
            if (_speed < MaxSpeed)
            {
                _speed += 2;
                _increaseWhen += 100;
            }
            else
            {
                _speed = MaxSpeed;
            }
        }

        Vector3 _moveForward = transform.forward * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + _moveForward);
    }

    public int GetDistanceTravelled()
    {
        return _distanceTravelled;
    }

    public int GetSpeed()
    {
        return _speed;
    }

    public int GetMaxSpeed()
    {
        return MaxSpeed;
    }

    public int GetPlayTime()
    {
        _endTime = System.DateTimeOffset.Now.ToUnixTimeSeconds();
        _playTime = _endTime - _startTime;
        return (int)_playTime;
    }

    public void SetSpeed(int SpeedChange)
    {
        Debug.Log("Speed");
        if (-SpeedChange < MaxSpeed)
        {
            _speed += SpeedChange;
        }
        Debug.Log("Speed");
    }
}
