using System;
using Mirror;
using UnityEngine;

public class RoleSelectionCanvas : MonoBehaviour
{
    [SerializeField] private NetworkGameMode _gameMode = NetworkGameMode.SinglePlayer;

    private PlayerType _selectedRole;

    private void Start()
    {
        ArgoNetworkManager.singleton.ClientConnect += OnClientConnect;
    }

    public void OnRoleBtnSelected(int role)
    {
        if (_gameMode == NetworkGameMode.SinglePlayer)
        {
            StartSinglePlayerGame((PlayerType) role);
        }
    }
    
    
    public void OnBackBtnClick()
    {
        gameObject.SetActive(false);
    }

    private void StartSinglePlayerGame(PlayerType role)
    {
        _selectedRole = role;
        ArgoNetworkManager.singleton.maxConnections = 1;
        ArgoNetworkManager.singleton.GameMode = NetworkGameMode.SinglePlayer;
        
        ArgoNetworkManager.singleton.StartHost();
    }
    
    private void OnClientConnect()
    {
        var roleSelectionMessage = new RoleSelectionMessage
        {
            Role = _selectedRole
        };

        NetworkClient.Send(roleSelectionMessage);
    }
}
