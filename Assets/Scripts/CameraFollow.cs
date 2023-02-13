// Coders:
// Caroline Percy
// ...

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

    /// A reference to the runner in the scene.
    [SerializeField] private Transform runner;

    /// The distance from the camera to the target (ie. the runner)
    private Vector3 _offset;

    /// <summary>
    /// Sets up offset from player so the camera is at a fixed distance from the player
    /// </summary>
    void Start()
    {
        _offset = transform.position - runner.position;
    }

    /// <summary>
    /// Makes the camera follow the player, runner and god
    /// </summary>
    void LateUpdate()
    {
        if (type == CameraType.Runner)
        {
            Vector3 newPos = runner.position + _offset;
            transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
        }
        else if (type == CameraType.God)
        {
            Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, runner.position.z + _offset.z);
            transform.position = targetPos;
        }
    }
}
