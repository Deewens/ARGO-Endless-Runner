// Coders:
// Caroline Percy
// ...

using Mirror;
using Mirror.Examples.SyncDir;
using UnityEngine;

/// <summary>
/// Enum that keeps track of what kind of camera this is.
/// </summary>
internal enum CameraType {
    Runner,
    God
}

/// <summary>
/// A class that lets the camera follow the Runner
/// </summary>
public class CameraFollow : MonoBehaviour
{
    /// What type of camera this one is.
    [SerializeField] private CameraType type = CameraType.Runner;
    
    private Transform _runner;
    public Transform Runner
    {
        set
        {
            _runner = value;
            _offset = transform.position - _runner.position;
        }
    }

    /// The distance from the camera to the target (ie. the runner)
    private Vector3 _offset;

    /// <summary>
    /// Makes the camera follow the player, runner and god
    /// </summary>
    void LateUpdate()
    {
        if (_runner == null)
            return;
        
        if (type == CameraType.Runner)
        {
            Vector3 newPos = _runner.position + _offset;
            transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
        }
        else if (type == CameraType.God)
        {
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, _runner.position.z + _offset.z);
            transform.position = targetPos;
        }
    }
}
