using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputManager : Singleton<InputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion
    private RunnerController runnerController;
    private Camera camera;

    void Awake()
    {
        runnerController = new RunnerController();
        camera = Camera.main;
    }

    void OnEnable()
    {
        runnerController.Enable();
    }

    void OnDisable()
    {
        runnerController.Disable();
    }
    void Start()
    {
        runnerController.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        runnerController.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(camera, runnerController.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null) OnStartTouch(Utils.ScreenToWorld(camera, runnerController.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(camera, runnerController.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
