/// Author : Patrick Donnelly
/// Contributors : ---

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/// <summary>
/// A class that moves the player forward and tracks the distance they have travelled
/// </summary>
public class MoveForward : MonoBehaviour
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
    private bool _isSpeedIncreased;

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
        _isSpeedIncreased = false;
    }


    /// <summary>
    /// Calculates the distance travelled by the runner and increases speed based on distance travelled
    /// </summary>
    void FixedUpdate()
    {
        _distanceTravelled = (int)transform.position.z - (int)_startingPos;

        if (_distanceTravelled % _increaseWhen == 0 && _distanceTravelled != 0)
        {
            if (_speed < MaxSpeed)
            {
                _speed += 2;
                _increaseWhen += 100;
                _isSpeedIncreased = true;
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

    public bool GetIsSpeedIncreased()
    {
        return _isSpeedIncreased;
    }

    public void SetIsSpeedIncreased(bool IsSpeedIncreased)
    {
        _isSpeedIncreased = IsSpeedIncreased;
    }

    public int GetPlayTime()
    {
        _endTime = System.DateTimeOffset.Now.ToUnixTimeSeconds();
        _playTime = _endTime - _startTime;
        return (int)_playTime;
    }
}
