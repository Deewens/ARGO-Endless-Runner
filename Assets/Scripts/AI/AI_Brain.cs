using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Brain : MonoBehaviour
{
    /// The Rigidbody of the Runner - Used to apply the force for the jump.
    Rigidbody rb;

    /// The amount of force applied to the Runner in order to get them off the ground.
    float jumpForce = 80.0f;

    /// Bool that keeps track of whether the Runner is on the ground or not.
    bool grounded = false;

    [Header("AI_Sliding")]
    /// Maximum slide time 
    public float maxSlideTime = 1.0f;
    /// How much force is applied to gameobject when sliding  
    public float slideForce;
    /// Timer to check how long the gameobject is sliding for before going back to running 
    private float slideTimer;

    /// The scale will be half of what the gameobject is to show that it is currently sliding 
    public float slideYScale;
    /// The scale will be double of what the gameobject is to show that it is currently sliding
    public float slideZScale;

    private Vector3 AIScale;

    public Transform orientation;
    public Transform AIObj;

    bool sliding;
    /// <summary>
    /// Controller of how the AI Runner reacts, once it sees an obstacle.
    /// </summary>
    /// <param name="t_seenObstacle">The obstacle it sees ahead.</param>
    public void React(Collider t_seenObstacle)
    {
        if (t_seenObstacle.CompareTag("JumpObstacle"))
        {
            Jump();
        }

        if (t_seenObstacle.CompareTag("SlideObstacle"))
        {
            StartSlide();
        }

        if (t_seenObstacle.CompareTag("Inpenetrable"))
        {
            // Move left or right
        }
    }

    /// <summary>
    /// A function that is called at the start of the game, to get the components in the Runner.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        AIScale = AIObj.localScale;
    }

    /// <summary>
    /// A check to see whether the Runner has landed back on the ground.
    /// </summary>
    /// <param name="collision">The object the Runner has collided with.</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            Land();
        }
    }

    /// <summary>
    /// What happens when the Runner is on the ground again.
    /// </summary>
    void Land()
    {
        grounded = true;
    }

    /// <summary>
    /// The physical motion for the runner to jump.
    /// </summary>
    void Jump()
    {
        if (grounded)
        {
            rb.AddForce(transform.up * jumpForce * 10);
            grounded = false;
        }
    }

    private void StartSlide()
    {
        sliding = true;

        AIObj.localScale = new Vector3(AIObj.localScale.x, slideYScale, slideZScale);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void StopSlide()
    {
        sliding = false;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        AIObj.localScale = AIScale;
    }

    private void Update()
    {
        if (sliding && slideTimer > 0)
        {
            slideTimer -= Time.deltaTime;
        }
        else
        {
            StopSlide();
        }
    }
}
