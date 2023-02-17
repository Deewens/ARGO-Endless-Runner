/// Author : Patrick Donnelly

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScroller : MonoBehaviour
{
    private Transform _runner;

    private Vector3 _offset;

    public Transform Runner
    {
        set
        {
            _runner = value;
            _offset = transform.position - _runner.position;
        }
    }

    void Start()
    {
        _runner = GameObject.Find("RunnerPlayer").transform;
        _offset = transform.position - _runner.position;
    }

    private void Update()
    {
        if(_runner == null)
        {
            Debug.Log("Cannot Find Runner in Scene - null reference");
            _runner = GameObject.Find("RunnerPlayer").transform;
            _offset = transform.position - _runner.position;
        }
    }

    void LateUpdate()
    {
        Vector3 newPos = _runner.position + _offset;
        transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
    }
}