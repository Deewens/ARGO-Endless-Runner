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

using UnityEngine;
using Mirror;

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
