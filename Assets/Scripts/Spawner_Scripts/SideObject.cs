using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that defines the side objects that line the path,
/// checks for collision to detect when they are off screen
/// </summary>
public class SideObject : MonoBehaviour
{
    private SideObjectSpawner _sideObjectSpawner;
    
    private void Start()
    {
        _sideObjectSpawner = FindObjectOfType<SideObjectSpawner>();
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Runner"))
        {
            StartCoroutine(ReplaceSideObject());
        }
    }
    
    /// <summary>
    /// Hide the object after 2 seconds and move it behind the player.
    /// </summary>
    private IEnumerator ReplaceSideObject()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        _sideObjectSpawner.MoveSideObject(this);
    }
}
