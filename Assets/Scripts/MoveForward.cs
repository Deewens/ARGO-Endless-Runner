using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float _speed;
    public float _maxSpeed;
    public Rigidbody _rb;
    int _distanceTravelled;
    int _startingPos;
    int _increaseWhen;

    // Start is called before the first frame update
    void Start()
    {
        _increaseWhen = 100;
        _speed = 8.0f;
        _maxSpeed = 30.0f;
        _startingPos = (int)transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(_speed);
        Debug.Log(_distanceTravelled);


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
