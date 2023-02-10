using System.Collections;
using UnityEngine;

public class RunnerPlayer : MonoBehaviour
{
    ///Minimum swipe distance needed to trigger movement
    [SerializeField]
    private float minimumDistance = 0.1f;

    /// Maximum time holding finger down on touchscreen
    [SerializeField]
    private float maximumTime = 1f;

    /// Used to compare angle of swipe to determine if side swipe or down swipe
    [SerializeField]
    private float directionThreshold = .7f;

    ///How much the player moves when swiped to the side
    [SerializeField]
    private float laneSize = 1;

    ///Max amount of lanes
    [SerializeField]
    private float laneCount = 3;

    ///Current lane that the player is occupying
    private float currentLane;

    private InputManager inputManager;

    ///Start position of swipe
    private Vector2 startPosition;

    ///Start time of swipe
    private float startTime;

    ///End position of swipe
    private Vector2 endPosition;

    ///End time of swipe
    private float endTime;

    [Header("Refrences")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    [Header("Sliding")]
    public float maxSlideTime = 1.0f;
    public float slideForce;
    private float slideTimer;
    public float slideYScale;
    public float slideZScale;
    private Vector3 playerScale;
    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    bool sliding;
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

    /// <summary>
    /// Trigger upon start of swipe, sets position and time variables
    /// </summary>
    /// <param name="position"></param>
    /// <param name="time"></param>
    private void SwipeStart(Vector2 position, float time)
    {
        startPosition = position;
        startTime = time;
    }

    /// <summary>
    /// Triggers upon end of swipe, sets end position and end time variables
    /// </summary>
    /// <param name="position"></param>
    /// <param name="time"></param>
    private void SwipeEnd(Vector2 position, float time)
    {
        endPosition = position;
        endTime = time;
        DetectSwipe();
    }

    /// <summary>
    /// Checks if start and end position reaches minimum distance, checks if start and end time is less than maximum allowed time
    /// </summary>
    private void DetectSwipe()
    {
        if (Vector3.Distance(startPosition, endPosition) >= minimumDistance &&
            (endTime - startTime) <= maximumTime)
        {
            Vector3 direction = endPosition - startPosition;
            Vector2 direction2D = new Vector2(direction.x, direction.x).normalized;
            SwipeDirection(direction2D);
        }
    }

    /// <summary>
    /// Determines what direction the swipe was towards
    /// </summary>
    /// <param name="direction"></param>
    private void SwipeDirection(Vector2 direction)
{
    if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
    {
        StartSlide();
    }
    if (currentLane > 1)
    {
        if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
            currentLane--;
        }

    }
    if (currentLane < laneCount)
    {
        if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
            currentLane++;
        }
    }
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
}