using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)]
public class InputManager : Singleton<InputManager>
{
    #region Events
    public delegate void StartTouch(Vector2 position, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 position, float time);
    public event EndTouch OnEndTouch;
    #endregion
    private RunnerController runnerController;
    private new Camera camera;

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
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        runnerController.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        runnerController.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        StartCoroutine(WaitStartTouch(context));
    }

    private IEnumerator WaitStartTouch(InputAction.CallbackContext context)
    {
        if (camera == null)
        {
            camera = Camera.main;
        }
        yield return new WaitForEndOfFrame();

        var position = runnerController.Touch.PrimaryPosition.ReadValue<Vector2>();
        if (position.x == 0 && position.y == 0)
        {
            yield return null;
        }

        Vector2 temp = runnerController.Touch.PrimaryPosition.ReadValue<Vector2>();
        if (OnStartTouch != null) OnStartTouch(Utils.ScreenToWorld(camera, runnerController.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.startTime);
    }

    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(camera, runnerController.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(camera, runnerController.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
