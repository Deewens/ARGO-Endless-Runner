//Author : Izabela Zelek, February 2023
//Author : Caroline Percy
// ...

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class which uses a neural network to create AI movement
/// The Brain of the AI Runner.
/// </summary>

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

    ///The current scale of the AI Runner.
    private Vector3 AIScale;

    ///
    public Transform AIObj;

    /// How much the runner moves when changing lanes
    private int laneSize = 2;

    /// max amount of lanes
    private int laneCount = 3;

    /// current lane occupied by runner
    private int currentLane = 2;

    /// Keeps track of whether the AI runner is currently sliding.
    bool _sliding;
    /// Public reference to _sliding.
    public bool sliding { get { return _sliding; } }


    /// <summary>
    /// Controller of how the AI Runner reacts, once it sees an obstacle.
    /// </summary>
    /// <param name="t_seenObstacle">The obstacle it sees ahead.</param>
    public void React(Collider t_seenObstacle)
    {
        bool solved = false;

        if (t_seenObstacle.CompareTag("Inpenetrable"))
        {
            ///If in the middle lane, randomise left or right
            float lane = laneCount / 2.0f;
            if (currentLane == Mathf.Ceil(lane))
            {
                int rand = Random.Range(1, 3);
                if (rand == 1)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                    currentLane--;
                    solved = true;
                }
                else if (rand == 2)
                {
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                    currentLane++;
                    solved = true;
                }
            }
            else if (currentLane > 1 && !solved)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x - laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                currentLane--;
                solved = true;
            }
            else if (currentLane < laneCount && !solved)
            {
                gameObject.transform.position = new Vector3(gameObject.transform.position.x + laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                currentLane++;
                solved = true;
            }
        }

        if (t_seenObstacle.CompareTag("JumpObstacle") && !solved)
        {
            Jump();
            solved = true;
        }

        
        if (t_seenObstacle.CompareTag("SlideObstacle") && !solved)
        {
            StartSlide();
            solved =true;
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
            rb.AddForce(transform.up * jumpForce * 5);
            grounded = false;
        }
    }

    /// <summary>
    /// Start the AI sliding.
    /// </summary>
    private void StartSlide()
    {
        _sliding = true;

        AIObj.localScale = new Vector3(AIObj.localScale.x, slideYScale, slideZScale);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    /// <summary>
    /// End the AI sliding.
    /// </summary>
    private void StopSlide()
    {
        _sliding = false;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        AIObj.localScale = AIScale;
    }


    /// <summary>
    /// Calls every frame to update the brain.
    /// </summary>
    private void Update()
    {
        if (_sliding && slideTimer > 0)
        {
            slideTimer -= Time.deltaTime;
        }
        else
        {
            StopSlide();
        }
    }
}
