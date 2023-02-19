/*
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