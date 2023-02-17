using Mirror;
using UnityEngine;

/// <summary>
/// Assign references to the runner to every script that needs it in the scene, because the runner is instantiated at runtime.
/// </summary>
public class RunnerReferencesSetter : NetworkBehaviour
{
    [Header("The UI linked to the runner")]
    [SerializeField]
    private GameObject uiPrefab;

    [Header("The camera linked to the runner")]
    [SerializeField]
    private CameraFollow cameraPrefab;

    private void Awake()
    {
        var obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
        if (obstacleSpawner != null)
        {
            obstacleSpawner.Runner = transform;
        }

        var analyticsManager = FindObjectOfType<AnalyticsManager>();
        if (analyticsManager != null)
        {
            analyticsManager.Runner = gameObject;
        }
        
        var pickupController = FindObjectOfType<PickupController>();
        if (pickupController != null)
        {
            pickupController.Runner = transform;
        }
        
        var collectibleController = FindObjectOfType<CollectibleController>();
        if (collectibleController != null)
        {
            collectibleController.Runner = gameObject;
        }

        var backGroundScroller = FindObjectOfType<BGScroller>();
        if (backGroundScroller != null)
        {
            backGroundScroller.Runner = transform;
        }

        var playerLight = FindObjectOfType<LightScroller>();
        if (playerLight != null)
        {
            playerLight.Runner = transform;
        }
    }

    public override void OnStartLocalPlayer()
    {
        GameObject ui = Instantiate(uiPrefab);
        var uiManager = ui.GetComponentInChildren<UIManager>();
        if (uiManager != null)
        {
            uiManager.Runner = gameObject;
        }

        CameraFollow cameraFollow = Instantiate(cameraPrefab);
        if (cameraFollow != null)
        {
            cameraFollow.Runner = transform;
        }
    }
}