using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using JetBrains.Annotations;

/// <summary>
/// 
/// </summary>

public class NetworkPlayerController : NetworkBehaviour
{
    CameraFollow cam;

    Quaternion godRotation = Quaternion.Euler(74.733f, 0, 0);
    Quaternion runnerRotation = Quaternion.Euler(10, 0, 0);

    Vector3 godPos = new Vector3(0, 13.81f, 1.86f);
    Vector3 runnerPos = new Vector3(0, 6, -10);

    //[SyncVar]
    //bool god = false;

    /// <summary>
    /// 
    /// </summary>
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        cam = Camera.main.GetComponent<CameraFollow>();

        if (FindObjectOfType<NetworkClientCount>().playersConnected > 1)
        {
            madeGod();
            //cam._player = GameObject.Find("RunnerPlayer").transform;
            //cam._offset = godPos;
            cam.transform.rotation = godRotation;
        }

        else
        {
            madeRunner();
            //cam._player = transform.Find("RunnerPlayer");
            //cam._offset = runnerPos;
            cam.transform.rotation = runnerRotation;
        }
        
        
    }

    //[Command]
    void madeGod()
    {
        transform.Find("GodPlayer").gameObject.SetActive(true);

    }

    //[Command]
    void madeRunner()
    {
        transform.Find("RunnerPlayer").gameObject.SetActive(true);

    }

    /// <summary>
    /// 
    /// </summary>
    void Update()
    {
        if (!isLocalPlayer) { return; }

        //cam.CameraUpdate();
        
    }
}
