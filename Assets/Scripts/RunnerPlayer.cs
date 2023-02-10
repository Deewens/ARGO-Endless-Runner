using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RunnerPlayer : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = 0.1f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField]
    private float directionThreshold = .7f;
    [SerializeField]
    private float laneSize = 1;
    [SerializeField]
    private float laneCount = 3;

    private float currentLane;
    private InputManager inputManager;

    private Vector2 startPosition;
    private float startTime;
    private Vector2 endPosition;
    private float endTime;

    [Header("Refrences")]
    public Transform orientation;
    public Transform playerObj;
    /// The Rigidbody of the Runner - Used to apply the force for actions.
    private Rigidbody rb;

    [Header("Sliding")]
    public float maxSlideTime = 1.0f;

    private float slideTimer;

    public float slideYScale;
    public float slideZScale;

    private Vector3 playerScale;


    /// The amount of force applied to the Runner in order to get them off the ground.
    float jumpForce = 80.0f;
    /// Bool that keeps track of whether the Runner is on the ground or not.
    bool grounded = false;
    /// How hard the User has to swipe up before the Runner jumps.
    float swipeIntensity = 70.0f;

    bool sliding;
    bool moving = false;

    

    void Awake()
    {
        inputManager = InputManager.Instance;
        currentLane = Mathf.Ceil(laneCount / 2);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        playerScale = playerObj.localScale;
    }

    void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
            (endTime - startTime) <= maximumTime)
        {
            Vector3 direction = endPosition - startPosition;
            //Vector2 direction2D = new Vector2(direction.x, direction.x).normalized;
            direction = direction.normalized;
            SwipeDirection(direction);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (!sliding)
        {
            if (currentLane > 1)
            {
                if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
                {
                    moving = true;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                    currentLane--;
                }

            }
            if (currentLane < laneCount)
            {
                if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
                {
                    moving = true;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                    currentLane++;
                }
            }
            if (Vector2.Dot(Vector2.down, direction) > directionThreshold && !moving)
            {
                StartSlide();
            }
        }
        moving = false;
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

    private void StartSlide()
    {
        sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, slideZScale);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;
    }

    private void StopSlide()
    {
        sliding = false;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        playerObj.localScale = playerScale;
    }

    private void Jump()
    {
        if (grounded && !sliding)
        {
            rb.AddForce(transform.up * jumpForce * 10);
            grounded = false;
        }
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
    /// Called every frame to update physics. is used for the swipe check.
    /// </summary>
    private void FixedUpdate()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);

            if (touch.deltaPosition.y > swipeIntensity)
            {
                Jump();
            }
        }
    }

}
