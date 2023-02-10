using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEditorInternal.Profiling;

public class ARGONetworkManager : NetworkManager
{
    NetworkClientCount n;

    private void Start()
    {
        base.Start();
        n = GetComponent<NetworkClientCount>();
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        n.playersConnected++;
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        n.playersConnected--;
    }

    public void HostButton()
    {
        networkAddress = "localhost";
        StartHost();
    }

    public void ClientButton()
    {
        networkAddress = "localhost";
        StartClient();
    }
}
