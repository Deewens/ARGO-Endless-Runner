// Coders:
// Caroline Percy
// ...

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

    ///
    private RunnerController runnerController;

    ///
    private new Camera camera;

    /// <summary>
    /// 
    /// </summary>
    void Awake()
    {
        runnerController = new RunnerController();
        camera = Camera.main;
    }

    /// <summary>
    /// 
    /// </summary>
    void OnEnable()
    {
        runnerController.Enable();
    }

    /// <summary>
    /// 
    /// </summary>
    void OnDisable()
    {
        runnerController.Disable();
    }

    /// <summary>
    /// 
    /// </summary>
    void Start()
    {
        camera = GameObject.Find("Runner Camera").GetComponent<Camera>();
        runnerController.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        runnerController.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    private void StartTouchPrimary(InputAction.CallbackContext context)
    {
        StartCoroutine(WaitStartTouch(context));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    private void EndTouchPrimary(InputAction.CallbackContext context)
    {
        if (OnEndTouch != null) OnEndTouch(Utils.ScreenToWorld(camera, runnerController.Touch.PrimaryPosition.ReadValue<Vector2>()), (float)context.time);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public Vector2 PrimaryPosition()
    {
        return Utils.ScreenToWorld(camera, runnerController.Touch.PrimaryPosition.ReadValue<Vector2>());
    }
}
