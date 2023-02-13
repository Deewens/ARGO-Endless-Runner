/// Author : Patrick Donnelly
/// Contributors : ---

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField]
    public float speed =0f;
    float width = 3410;
    public bool scrollLeft = false;
    public bool scrollRight = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (scrollLeft)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;

            if (transform.position.x <= -width)
            {
                SetPosition(width);
            }
        }
        else if(scrollRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

            if (transform.position.x >= width)
            {
                SetPosition(-width);
            }
        }
    }

    public void SetSpeed(float t_speed)
    {
        speed = t_speed;
    }
    public void SetPosition(float t_position)
    {
        transform.position = new Vector3(t_position, transform.position.y, 0f); 
    }
}
