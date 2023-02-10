using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that moves the obstacles towards the player
/// </summary>
public class ObstacleMover : MonoBehaviour
{
    public Transform targetPos;
    public Transform startPos;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    /// <summary>
    /// moves an obstacle towards the player
    /// </summary>
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos.position, 0.05f);

        if(transform.position == targetPos.position )
        {
            transform.position = startPos.position;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Runner"))
        {
            Debug.Log("hit");
        }
    }
}
