using System;
using Mirror;
using UnityEngine;

/// <summary>
/// Open the role selector menu. When opening this menu, we assume that client hosted a server or joined one.
/// </summary>
public class RoleSelectionCanvas : MonoBehaviour
{
    private NetworkGameMode _gameMode;
    
    private NetworkManagerMode _networkMode;
    private Uri _serverUri;

    public void SetGameMode(NetworkGameMode gameMode)
    {
        _gameMode = gameMode;
    }

    public void SetNetworkMode(NetworkManagerMode networkMode)
    {
        _networkMode = networkMode;
    }

    public void SetServerUri(Uri serverUri)
    {
        _serverUri = serverUri;
    }
    
    public void OnRoleBtnSelected(int role)
    {
        if (_gameMode == NetworkGameMode.SinglePlayer)
        {
            ArgoNetworkManager.singleton.GameMode = NetworkGameMode.SinglePlayer;
            ArgoNetworkManager.singleton.maxConnections = 1;
            ArgoNetworkManager.singleton.StartHost();
        }
        else if (_gameMode == NetworkGameMode.MultiPlayer)
        {
            if (_networkMode == NetworkManagerMode.Host)
            {
                ArgoNetworkManager.singleton.GameMode = NetworkGameMode.MultiPlayer;
                ArgoNetworkManager.singleton.maxConnections = 2;
                ArgoNetworkManager.singleton.StartHost();
            }
            else if (_networkMode == NetworkManagerMode.ClientOnly)
            {
                ArgoNetworkManager.singleton.GameMode = NetworkGameMode.MultiPlayer;
                ArgoNetworkManager.singleton.maxConnections = 2;
                ArgoNetworkManager.singleton.StartClient(_serverUri);
            }
        }
    }

    public void OnBackBtnClick()
    {
        gameObject.SetActive(false);
    }
}