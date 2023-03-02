using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using System.Linq;

/*
    Documentation: https://mirror-networking.gitbook.io/docs/components/network-authenticators
    API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkAuthenticator.html
*/

public class ArgoNetworkAuthenticator : NetworkAuthenticator
{
    private readonly HashSet<NetworkConnection> _connectionsPendingDisconnect = new HashSet<NetworkConnection>();
    
    private PlayerType _playerRole;
    public PlayerType PlayerRole => _playerRole;

    #region Messages

    public struct AuthRequestMessage : NetworkMessage
    {
        public PlayerType PlayerRole;
    }

    public struct AuthResponseMessage : NetworkMessage
    {
        public byte Code;
        public string Message;
    }


    #endregion

    #region Server

    /// <summary>
    /// Called on server from StartServer to initialize the Authenticator
    /// <para>Server message handlers should be registered in this method.</para>
    /// </summary>
    public override void OnStartServer()
    {
        // register a handler for the authentication request we expect from client
        NetworkServer.RegisterHandler<AuthRequestMessage>(OnAuthRequestMessage, false);
    }

    /// <summary>
    /// Called on server from OnServerConnectInternal when a client needs to authenticate
    /// </summary>
    /// <param name="conn">Connection to client.</param>
    public override void OnServerAuthenticate(NetworkConnectionToClient conn) { }

    /// <summary>
    /// Called on server when the client's AuthRequestMessage arrives
    /// </summary>
    /// <param name="conn">Connection to client.</param>
    /// <param name="msg">The message payload</param>
    public void OnAuthRequestMessage(NetworkConnectionToClient conn, AuthRequestMessage msg)
    {
        Debug.Log($"Authentication Request: {msg.PlayerRole}");
        
        if (_connectionsPendingDisconnect.Contains(conn)) return;

        var isPlayerExists = ArgoNetworkManager.singleton.SpawnedPlayers.Exists(spawnedPlayer =>
            spawnedPlayer.IsAI == false && spawnedPlayer.PlayerType == msg.PlayerRole);
        if (!isPlayerExists)
        {
            conn.authenticationData = msg.PlayerRole;
            
            AuthResponseMessage authResponseMessage = new AuthResponseMessage
            {
                Code = 1,
                Message = "Success"
            };
            conn.Send(authResponseMessage);
            
            // Accept the successful authentication
            ServerAccept(conn);
        }
        else
        {
            _connectionsPendingDisconnect.Add(conn);

            // create and send msg to client so it knows to disconnect
            AuthResponseMessage authResponseMessage = new AuthResponseMessage
            {
                Code = 0,
                Message = "A player with this role is already in game, choose another one."
            };

            conn.Send(authResponseMessage);

            // must set NetworkConnection isAuthenticated = false
            conn.isAuthenticated = false;

            // disconnect the client after 1 second so that response message gets delivered
            StartCoroutine(DelayedDisconnect(conn, 1f));
        }
    }
    
    IEnumerator DelayedDisconnect(NetworkConnectionToClient conn, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // Reject the unsuccessful authentication
        ServerReject(conn);

        yield return null;

        // remove conn from pending connections
        _connectionsPendingDisconnect.Remove(conn);
    }

    #endregion

    #region Client
    
    /// <summary>
    /// Set the role of the player to be spawn in game
    /// </summary>
    /// <remarks>Called by role selector UI</remarks>
    /// <param name="role"></param>
    public void SetRole(int role)
    {
        _playerRole = (PlayerType) role;
    }


    /// <summary>
    /// Called on client from StartClient to initialize the Authenticator
    /// <para>Client message handlers should be registered in this method.</para>
    /// </summary>
    public override void OnStartClient()
    {
        // register a handler for the authentication response we expect from server
        NetworkClient.RegisterHandler<AuthResponseMessage>(OnAuthResponseMessage, false);
    }

    /// <summary>
    /// Called on client from OnClientConnectInternal when a client needs to authenticate
    /// </summary>
    public override void OnClientAuthenticate()
    {
        AuthRequestMessage authRequestMessage = new AuthRequestMessage
        {
            PlayerRole = _playerRole
        };

        NetworkClient.Send(authRequestMessage);
    }

    /// <summary>
    /// Called on client when the server's AuthResponseMessage arrives
    /// </summary>
    /// <param name="msg">The message payload</param>
    public void OnAuthResponseMessage(AuthResponseMessage msg)
    {
        if (msg.Code == 1)
        {
            Debug.Log($"Authentication Response: {msg.Code} {msg.Message}");

            // Authentication has been accepted
            ClientAccept();
            
            var roleSelectedMessage = new RoleSelectedMessage
            {
                Role = _playerRole
            };
            NetworkClient.Send(roleSelectedMessage);
        }
        else
        {
            Debug.LogError($"Authentication Response: {msg.Code} {msg.Message}");

            // Authentication has been rejected
            // StopHost works for both host client and remote clients
            NetworkManager.singleton.StopHost();
        }
    }

    #endregion
}
