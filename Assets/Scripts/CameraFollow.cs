using Mirror.Examples.SyncDir;
using UnityEngine;

internal enum CameraType {
    Runner,
    God
}

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CameraType type = CameraType.Runner;
    [SerializeField] private Transform runner;

    [Header("God Camera Settings (only if type is God)")]
    [SerializeField] private float distanceFromGod = 10;
    [SerializeField] private float heightFromGod = 25;

    private Vector3 _offset;
    private Vector3 _currentVelocity;

    void Start()
    {
        if (type == CameraType.Runner)
            _offset = transform.position - runner.position;
    }

    void LateUpdate()
    {
        if (type == CameraType.Runner)
        {
            Vector3 newPos = runner.position + _offset;
            transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
        }
        else if (type == CameraType.God)
        {
            Vector3 targetPos = runner.transform.position + (-runner.transform.forward * distanceFromGod);
            targetPos += Vector3.up * heightFromGod;
            transform.position = targetPos;
            transform.LookAt(runner);
        }
    }
}
