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

    void Awake()
    {
        inputManager = InputManager.Instance;
        currentLane = Mathf.Ceil(laneCount / 2);
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
}
