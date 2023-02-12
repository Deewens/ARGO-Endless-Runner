using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// A class which accepts player input and makes the runner move, jump and slide accordingly
/// </summary>
public class RunnerPlayer : MonoBehaviour
{
    [SerializeField]
    private float minimumDistance = 0.01f;
    [SerializeField]
    private float maximumTime = 1f;
    [SerializeField]
    private float directionThreshold = .7f;
    [SerializeField]
    private float laneSize = 3;
    [SerializeField]
    private float laneCount = 3;

    private float _currentLane;
    private InputManager _inputManager;

    private Vector2 _startPosition;
    private float _startTime;
    private Vector2 _endPosition;
    private float _endTime;

    [Header("Refrences")]
    public Transform orientation;
    public Transform playerObj;
    /// The Rigidbody of the Runner - Used to apply the force for actions.
    private Rigidbody _rb;

    [Header("Sliding")]
    public float maxSlideTime = 1.0f;

    private float _slideTimer;

    public float slideYScale;
    public float slideZScale;

    private Vector3 _playerScale;


    /// The amount of force applied to the Runner in order to get them off the ground.
    private float _jumpForce = 80.0f;
    /// Bool that keeps track of whether the Runner is on the ground or not.
    private bool _grounded = false;
    /// How hard the User has to swipe up before the Runner jumps.
    private float _swipeIntensity = 70.0f;

    private bool _sliding;
    public bool sliding { get { return _sliding; } }
    private bool _moving = false;

    

    void Awake()
    {
        _inputManager = InputManager.Instance;
        _currentLane = Mathf.Ceil(laneCount / 2);
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _playerScale = playerObj.localScale;
    }

    void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    public void SwipeStart(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;
    }

    public void SwipeEnd(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        if (Vector3.Distance(_startPosition, _endPosition) >= minimumDistance &&
            (_endTime - _startTime) <= maximumTime)
        {
            Vector3 direction = _endPosition - _startPosition;
            //Vector2 direction2D = new Vector2(direction.x, direction.x).normalized;
            direction = direction.normalized;
            SwipeDirection(direction);
        }
    }

    private void SwipeDirection(Vector2 direction)
    {
        if (!_sliding)
        {
            if (_currentLane > 1)
            {
                if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
                {
                    _moving = true;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x - laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                    _currentLane--;
                }

            }
            if (_currentLane < laneCount)
            {
                if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
                {
                    _moving = true;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x + laneSize, gameObject.transform.position.y, gameObject.transform.position.z);
                    _currentLane++;
                }
            }
            if (Vector2.Dot(Vector2.down, direction) > directionThreshold && !_moving)
            {
                StartSlide();
            }
        }
        _moving = false;
    }

    private void Update()
    {
        if (_sliding && _slideTimer > 0)
        {
            _slideTimer -= Time.deltaTime;
        }
        else
        {
            StopSlide();
        }
    }

    private void StartSlide()
    {
        _sliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, slideZScale);
        _rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        _slideTimer = maxSlideTime;
    }

    private void StopSlide()
    {
        _sliding = false;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        playerObj.localScale = _playerScale;
    }

    public void Jump()
    {
        if (_grounded && !_sliding)
        {
            _rb.AddForce(transform.up * _jumpForce * 6);
            _grounded = false;
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
        _grounded = true;
    }

    /// <summary>
    /// Called every frame to update physics. is used for the swipe check.
    /// </summary>
    private void FixedUpdate()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);

            if (touch.deltaPosition.y > _swipeIntensity)
            {
                Jump();
            }
        }
    }

}
