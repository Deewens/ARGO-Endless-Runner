using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

/// <summary>
/// A class that moves the player forward and tracks the distance they have travelled
/// </summary>
public class MoveForward : MonoBehaviour
{
    public float _speed;
    public float _maxSpeed;
    public Rigidbody _rb;
    public int _distanceTravelled;
    int _startingPos;
    int _increaseWhen;
    public int _obstaclesAvoided;

    /// <summary>
    /// Sets up default values
    /// </summary>
    void Start()
    {
        _increaseWhen = 100;
        _speed = 8.0f;
        _maxSpeed = 30.0f;
        _startingPos = (int)transform.position.z;
        _obstaclesAvoided= 0;
    }


    /// <summary>
    /// Calculates the distance travelled by the runner and increases speed based on distance travelled
    /// </summary>
    void FixedUpdate()
    {

        _distanceTravelled = (int)transform.position.z - (int)_startingPos;

        if (_distanceTravelled % _increaseWhen == 0 && _distanceTravelled != 0)
        {
            if (_speed < _maxSpeed)
            {
                _speed += 2.0f;
                _increaseWhen += 100;
            }
            else
            {
                _speed = _maxSpeed;
            }
        }

        Vector3 _moveForward = transform.forward * _speed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + _moveForward);
    }
}
