using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMover : MonoBehaviour
{
    public Transform targetPos;
    public Transform startPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
