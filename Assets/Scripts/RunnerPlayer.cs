﻿/*
Olympus Run - A game made as part of the ARGO Project at SETU Carlow
Copyright (C) 2023 Caroline Percy <lineypercy@me.com>, Patrick Donnelly <patrickdonnelly3759@gmail.com>, Izabela Zelek <C00247865@itcarlow.ie>, Danial-hakim <danialhakim01@gmail.com>, Adrien Dudon <dudonadrien@gmail.com>

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program.  If not, see <https://www.gnu.org/licenses/>.
*/

using Mirror;
using UnityEngine;
/// <summary>
/// A class which accepts player input and makes the runner move, jump and slide accordingly
/// </summary>
public class RunnerPlayer : NetworkBehaviour
{
    /// The minimum distance the swipe is to be to be considered a swipe
    [SerializeField]
    private float minimumDistance = 0.01f;

    /// The max amount of time the swipe can last
    [SerializeField]
    private float maximumTime = 1f;

    /// the minimum amount of movement to move a direction.
    [SerializeField]
    private float directionThreshold = .7f;

    /// The size of each lane.
    [SerializeField]
    private float laneSize = 3;

    /// The amount of lanes there are.
    [SerializeField]
    private float laneCount = 3;

    /// The current lane the runner is in.
    private float _currentLane;

    /// Is used to getect swipes on the screen.
    private InputManager _inputManager;

    /// The start of the swipe.
    private Vector2 _startPosition;

    /// The time the swipe started.
    private float _startTime;

    /// The end of the swipe.
    private Vector2 _endPosition;

    /// The time the swipe ended.
    private float _endTime;

    // Reference to this object's transform
    // TODO: What is this for?
    [Header("References")]
    public Transform playerObj;

    /// The Rigidbody of the Runner - Used to apply the force for actions.
    private Rigidbody _rb;

    [Header("Sliding")]
    public float maxSlideTime = 1.0f;

    /// The timer that keeps track of how long the Runner has been sliding.
    private float _slideTimer;

    /// The Y scale the Runner takes on while sliding.
    public float slideYScale;

    /// The X scale the Runner takes on while sliding.
    public float slideZScale;

    /// The Runner's original scale.
    private Vector3 _playerScale;

    /// The amount of force applied to the Runner in order to get them off the ground.
    private float _jumpForce = 80.0f;
    /// Bool that keeps track of whether the Runner is on the ground or not.
    private bool _grounded = false;
    /// How hard the User has to swipe up before the Runner jumps.
    private float _swipeIntensity = 70.0f;

    /// Whether the runner is currently jumping
    private bool _jumping;
    /// Whether the runner is currently sliding.
    private bool _sliding;
    public bool sliding { get { return _sliding; } }

    /// Whether the Runner is moving.
    private bool _moving = false;
    public bool jumping { get { return _jumping; } }

    void Awake()
    {
        _inputManager = InputManager.Instance;
        _currentLane = Mathf.Ceil(laneCount / 2);
    }

    /// <summary>
    /// Is called after Awake. Gets the rigidbody component.
    /// </summary>
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerScale = playerObj.localScale;
    }

    private void OnEnable()
    {
        _inputManager.OnStartTouch += SwipeStart;
        _inputManager.OnEndTouch += SwipeEnd;
    }

    /// <summary>
    /// Is called when the screen has stopped being touched.
    /// </summary>
    private void OnDisable()
    {
        _inputManager.OnStartTouch -= SwipeStart;
        _inputManager.OnEndTouch -= SwipeEnd;
    }

    /// <summary>
    /// Is called to get the numbers of the swipe.
    /// </summary>
    /// <param name="position">The starting position of the swipe.</param>
    /// <param name="time">The start time of the swipe.</param>
    private void SwipeStart(Vector2 position, float time)
    {
        _startPosition = position;
        _startTime = time;
    }

    /// <summary>
    /// Is called to get the numbers of the swipe, and pass it on.
    /// </summary>
    /// <param name="position">The end position of the swipe.</param>
    /// <param name="time">The end time of the swipe.</param>
    private void SwipeEnd(Vector2 position, float time)
    {
        _endPosition = position;
        _endTime = time;
        DetectSwipe();
    }

    /// <summary>
    /// Figures out whether the input is a proper swipe.
    /// </summary>
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

    /// <summary>
    /// Figures out what the swipe will do to the runner.
    /// </summary>
    /// <param name="direction">The direction the swipe.</param>
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
                _sliding = true;
                transform.GetChild(0).GetComponent<PointOfInterest>().StartSlide();
                StartSlide();
            }
        }
        _moving = false;
    }

    /// <summary>
    /// The update that is called every frame for the Runner.
    /// </summary>
    private void Update()
    {
        if (!isLocalPlayer)
            return;
        
        if (_sliding && _slideTimer > 0)
        {
            _slideTimer -= Time.deltaTime;
        }
        else
        {
            StopSlide();
        }
    }

    /// <summary>
    /// Changes the Runner's state to sliding.
    /// </summary>
    private void StartSlide()
    {
        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, slideZScale);
        _rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        _slideTimer = maxSlideTime;
    }

    /// <summary>
    /// Resets the Runner back to its running state.
    /// </summary>
    private void StopSlide()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        playerObj.localScale = _playerScale;
        _sliding = false;

    }

    /// <summary>
    /// Performs the jump command.
    /// </summary>
    private void Jump()
    {
        if (_grounded && !_sliding)
        {
            transform.GetChild(0).GetComponent<PointOfInterest>().StartJump();
            _jumping = true;
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
        if (!isLocalPlayer)
            return;
        
        if (collision.transform.CompareTag("Ground"))
        {
            Land();
        }
    }

    /// <summary>
    /// What happens when the Runner is on the ground again.
    /// </summary>
    private void Land()
    {
        _grounded = true;
        _jumping = false;
    }

    /// <summary>
    /// Called every frame to update physics. is used for the swipe check.
    /// </summary>
    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
        
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
