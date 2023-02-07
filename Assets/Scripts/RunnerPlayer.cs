using System.Collections;
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
            Vector2 direction2D = new Vector2(direction.x, direction.x).normalized;
            SwipeDirection(direction2D);
        }
    }

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
