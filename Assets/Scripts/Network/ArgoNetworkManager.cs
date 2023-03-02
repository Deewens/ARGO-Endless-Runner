/*
Olympus Run - A game made as part of the ARGO Project at SETU Carlow
Copyright (C) 2023 Caroline Percy <lineypercy@me.com>, Patrick Donnelly <patrickdonnelly3759@gmail.com>, Izabela Zelek <izabelawzelek@gmail.com>, Danial-hakim <danialhakim01@gmail.com>, Adrien Dudon <dudonadrien@gmail.com>

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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/components/network-manager
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkManager.html
*/

public class ArgoNetworkManager : NetworkManager
{
    // Overrides the base singleton so we don't
    // have to cast to this type everywhere.
    public new static ArgoNetworkManager singleton { get; private set; }

    private SceneOperation _clientSceneOperation;

    public readonly List<Player> SpawnedPlayers = new List<Player>();

    public NetworkGameMode GameMode { get; set; } = NetworkGameMode.SinglePlayer;

    /// <summary>
    /// Runs on both Server and Client
    /// Networking is NOT initialized when this fires
    /// </summary>
    public override void Awake()
    {
        base.Awake();
        singleton = this;
    }

    #region Unity Callbacks

    public override void OnValidate()
    {
        base.OnValidate();
    }

    /// <summary>
    /// Runs on both Server and Client
    /// Networking is NOT initialized when this fires
    /// </summary>
    public override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// Runs on both Server and Client
    /// </summary>
    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    /// <summary>
    /// Runs on both Server and Client
    /// </summary>
    public override void OnDestroy()
    {
        base.OnDestroy();
    }

    #endregion

    #region Start & Stop

    /// <summary>
    /// Set the frame rate for a headless server.
    /// <para>Override if you wish to disable the behavior or set your own tick rate.</para>
    /// </summary>
    public override void ConfigureHeadlessFrameRate()
    {
        base.ConfigureHeadlessFrameRate();
    }

    /// <summary>
    /// called when quitting the application by closing the window / pressing stop in the editor
    /// </summary>
    public override void OnApplicationQuit()
    {
        base.OnApplicationQuit();
    }

    #endregion

    #region Scene Management

    /// <summary>
    /// This causes the server to switch scenes and sets the networkSceneName.
    /// <para>Clients that connect to this server will automatically switch to this scene. This is called automatically if onlineScene or offlineScene are set, but it can be called from user code to switch scenes again while the game is in progress. This automatically sets clients to be not-ready. The clients must call NetworkClient.Ready() again to participate in the new scene.</para>
    /// </summary>
    /// <param name="newSceneName"></param>
    public override void ServerChangeScene(string newSceneName)
    {
        SpawnedPlayers.Clear();

        base.ServerChangeScene(newSceneName);
    }

    /// <summary>
    /// Called from ServerChangeScene immediately before SceneManager.LoadSceneAsync is executed
    /// <para>This allows server to do work / cleanup / prep before the scene changes.</para>
    /// </summary>
    /// <param name="newSceneName">Name of the scene that's about to be loaded</param>
    public override void OnServerChangeScene(string newSceneName)
    {
    }

    /// <summary>
    /// Called on the server when a scene is completed loaded, when the scene load was initiated by the server with ServerChangeScene().
    /// </summary>
    /// <param name="sceneName">The name of the new scene.</param>
    public override void OnServerSceneChanged(string sceneName)
    {
    }

    /// <summary>
    /// Called from ClientChangeScene immediately before SceneManager.LoadSceneAsync is executed
    /// <para>This allows client to do work / cleanup / prep before the scene changes.</para>
    /// </summary>
    /// <param name="newSceneName">Name of the scene that's about to be loaded</param>
    /// <param name="sceneOperation">Scene operation that's about to happen</param>
    /// <param name="customHandling">true to indicate that scene loading will be handled through overrides</param>
    public override void OnClientChangeScene(string newSceneName, SceneOperation sceneOperation, bool customHandling)
    {
        _clientSceneOperation = sceneOperation;
    }

    /// <summary>
    /// Called on clients when a scene has completed loaded, when the scene load was initiated by the server.
    /// <para>Scene changes can cause player objects to be destroyed. The default implementation of OnClientSceneChanged in the NetworkManager is to add a player object for the connection if no player object exists.</para>
    /// </summary>
    public override void OnClientSceneChanged()
    {
        base.OnClientSceneChanged();

        // Only call AddPlayer for normal scene changes, not additive load/unload
        if (NetworkClient.connection.isAuthenticated && _clientSceneOperation == SceneOperation.Normal &&
            NetworkClient.localPlayer == null)
        {
            ArgoNetworkAuthenticator customAuth = (ArgoNetworkAuthenticator) authenticator;
            var roleSelectedMessage = new RoleSelectedMessage
            {
                Role = customAuth.PlayerRole
            };
            NetworkClient.Send(roleSelectedMessage);
            // add player if existing one is null
            //NetworkClient.AddPlayer();
        }
    }

    #endregion

    #region Server System Callbacks

    /// <summary>
    /// Called on the server when a new client connects.
    /// <para>Unity calls this on the Server when a Client connects to the Server. Use an override to tell the NetworkManager what to do when a client connects to the server.</para>
    /// </summary>
    /// <param name="conn">Connection from client.</param>
    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
    }

    /// <summary>
    /// Called on the server when a client is ready.
    /// <para>The default implementation of this function calls NetworkServer.SetClientReady() to continue the network setup process.</para>
    /// </summary>
    /// <param name="conn">Connection from client.</param>
    public override void OnServerReady(NetworkConnectionToClient conn)
    {
        base.OnServerReady(conn);
    }

    /// <summary>
    /// Called on the server when a client disconnects.
    /// <para>This is called on the Server when a Client disconnects from the Server. Use an override to decide what should happen when a disconnection is detected.</para>
    /// </summary>
    /// <param name="conn">Connection from client.</param>
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        var disconnectingPlayer = SpawnedPlayers.Find(player => player.netIdentity.connectionToClient == conn);
        if (disconnectingPlayer.PlayerType == PlayerType.Runner)
        {
            // Spawn a runner AI
            GameObject runnerAI = Instantiate(spawnPrefabs[0]);
            runnerAI.name = $"{spawnPrefabs[0].name} [AI]";
            runnerAI.transform.GetChild(1).gameObject.SetActive(true);
            runnerAI.GetComponent<AIBrain>().enabled = true;
            runnerAI.GetComponent<RunnerPlayer>().enabled = false;
            runnerAI.GetComponent<Player>().IsAI = true;
                
            NetworkServer.Spawn(runnerAI);
                
            AddPlayerToSpawnedList(runnerAI);
        }
        else if (disconnectingPlayer.PlayerType == PlayerType.God)
        {
            try
            { 
                var spawnedRunner = SpawnedPlayers.Find(spawnedPlayer => spawnedPlayer.PlayerType == PlayerType.Runner);

                // Activate the GameObject required by the God AI in the Runner GO.
                var aiGodPlacementGO = spawnedRunner.GetComponentInChildren<AI_God_ViewForward>(true).gameObject;
                aiGodPlacementGO.SetActive(true);

                var spawnedRunnerPos = spawnedRunner.transform.position;
                var defaultGodHeight = spawnPrefabs[1].transform.position.y;
                
                // Spawn the god at the same pos as the player because code of the god is garbage!!!!
                GameObject godAI = Instantiate(spawnPrefabs[1], new Vector3(spawnedRunnerPos.x, defaultGodHeight, spawnedRunnerPos.z), Quaternion.identity);
                godAI.name = $"{spawnPrefabs[1].name} [AI]";
                var aiGodScript = godAI.GetComponent<AIGod>();
                aiGodScript.enabled = true;
                godAI.GetComponent<Player>().IsAI = true;

                NetworkServer.Spawn(godAI);
                AddPlayerToSpawnedList(godAI);
            }
            catch (ArgumentNullException)
            {
                Debug.LogError("No runner spawned in the game.");
                StopServer();
            }
        }
        
        SpawnedPlayers.Remove(disconnectingPlayer);
        base.OnServerDisconnect(conn);
    }

    /// <summary>
    /// Called on server when transport raises an exception.
    /// <para>NetworkConnection may be null.</para>
    /// </summary>
    /// <param name="conn">Connection of the client...may be null</param>
    /// <param name="exception">Exception thrown from the Transport.</param>
    public override void OnServerError(NetworkConnectionToClient conn, TransportError transportError, string message)
    {
    }

    #endregion

    #region Client System Callbacks

    /// <summary>
    /// Called on the client when connected to a server.
    /// <para>The default implementation of this function sets the client as ready and adds a player. Override the function to dictate what happens when the client connects.</para>
    /// </summary>
    public override void OnClientConnect()
    {
        base.OnClientConnect();
    }

    /// <summary>
    /// Called on clients when disconnected from a server.
    /// <para>This is called on the client when it disconnects from the server. Override this function to decide what happens when the client disconnects.</para>
    /// </summary>
    public override void OnClientDisconnect()
    {
    }

    /// <summary>
    /// Called on clients when a servers tells the client it is no longer ready.
    /// <para>This is commonly used when switching scenes.</para>
    /// </summary>
    public override void OnClientNotReady()
    {
    }

    /// <summary>
    /// Called on client when transport raises an exception.</summary>
    /// </summary>
    /// <param name="exception">Exception thrown from the Transport.</param>
    public override void OnClientError(TransportError transportError, string message)
    {
    }

    #endregion

    #region Start & Stop Callbacks

    // Since there are multiple versions of StartServer, StartClient and StartHost, to reliably customize
    // their functionality, users would need override all the versions. Instead these callbacks are invoked
    // from all versions, so users only need to implement this one case.

    /// <summary>
    /// This is invoked when a host is started.
    /// <para>StartHost has multiple signatures, but they all cause this hook to be called.</para>
    /// </summary>
    public override void OnStartHost()
    {
    }

    /// <summary>
    /// This is invoked when a server is started - including when a host is started.
    /// <para>StartServer has multiple signatures, but they all cause this hook to be called.</para>
    /// </summary>
    public override void OnStartServer()
    {
        base.OnStartServer();

        NetworkServer.RegisterHandler<RoleSelectedMessage>(OnPlayerRoleSelected);
    }

    private void OnPlayerRoleSelected(NetworkConnectionToClient conn, RoleSelectedMessage msg)
    {
        Debug.Log("TEST ON PLAYER ROLE SLEECTED");
        if (msg.Role == PlayerType.Runner)
        {
            var isRunnerAISpawned = SpawnedPlayers.Exists(spawnedPlayer => spawnedPlayer.PlayerType == PlayerType.Runner && spawnedPlayer.IsAI == true);
            if (isRunnerAISpawned)
            {
                var runnerToDestroy =
                    SpawnedPlayers.Find(spawnedPlayer => spawnedPlayer.PlayerType == PlayerType.Runner);
                NetworkServer.Destroy(runnerToDestroy.gameObject);
            }
            
            GameObject player = Instantiate(spawnPrefabs[0]);
            player.name = $"{spawnPrefabs[0].name} [connId={conn.connectionId}]";

            bool isGodSpawned = SpawnedPlayers.Exists(spawnedPlayer => spawnedPlayer.PlayerType == PlayerType.God && spawnedPlayer.IsAI == false);
            if (!isGodSpawned)
            {
                // Activate the GameObject required by the God AI in the Runner GO.
                var aiGodPlacementGO = player.GetComponentInChildren<AI_God_ViewForward>(true).gameObject;
                aiGodPlacementGO.SetActive(true);
            }

            NetworkServer.AddPlayerForConnection(conn, player);
            AddPlayerToSpawnedList(player);

            if (!isGodSpawned)
            {
                GameObject godAI = Instantiate(spawnPrefabs[1]);
                godAI.name = $"{spawnPrefabs[1].name} [AI]";
                var aiGodScript = godAI.GetComponent<AIGod>();
                aiGodScript.enabled = true;
                godAI.GetComponent<Player>().IsAI = true;

                NetworkServer.Spawn(godAI);
                AddPlayerToSpawnedList(godAI);
            }
            else
            {
                // TODO: It's garbage, needs to find a way to properly reset the game but for now it's just bugged 
                //var spawnedGod = SpawnedPlayers.Find(spawnedPlayer => spawnedPlayer.PlayerType == PlayerType.God && spawnedPlayer.IsAI == false);
                
                //if (isGameRestarted)
                // Player spawn as a runner but there is a god in the game, we need to restart the game
                //ServerChangeScene(networkSceneName);
            }
        }
        else
        {
            var isGodAISpawned = SpawnedPlayers.Exists(spawnedPlayer => spawnedPlayer.PlayerType == PlayerType.God && spawnedPlayer.IsAI == true);
            if (isGodAISpawned)
            {
                var godToDestroy =
                    SpawnedPlayers.Find(spawnedPlayer => spawnedPlayer.PlayerType == PlayerType.God);
                NetworkServer.Destroy(godToDestroy.gameObject);
            }
            
            var isRunnerSpawned = SpawnedPlayers.Exists(spawnedPlayer => spawnedPlayer.PlayerType == PlayerType.Runner && spawnedPlayer.IsAI == false);
            if (!isRunnerSpawned)
            {
                // Spawn a runner AI
                GameObject runnerAI = Instantiate(spawnPrefabs[0]);
                runnerAI.name = $"{spawnPrefabs[0].name} [AI]";
                runnerAI.transform.GetChild(1).gameObject.SetActive(true);
                runnerAI.GetComponent<AIBrain>().enabled = true;
                runnerAI.GetComponent<RunnerPlayer>().enabled = false;
                runnerAI.GetComponent<Player>().IsAI = true;
                
                NetworkServer.Spawn(runnerAI);
                
                AddPlayerToSpawnedList(runnerAI);
            }
            else
            {
                var spawnedRunner =
                    SpawnedPlayers.Find(spawnedPlayer => spawnedPlayer.PlayerType == PlayerType.Runner && spawnedPlayer.IsAI == false);
                spawnedRunner.GetComponentInChildren<AI_God_ViewForward>().gameObject.SetActive(false);
            }
            

            var player = Instantiate(spawnPrefabs[1]);
            player.name = $"{spawnPrefabs[1].name} [connId={conn.connectionId}]";

            NetworkServer.AddPlayerForConnection(conn, player);
            AddPlayerToSpawnedList(player);
        }
    }

    /// <summary>
    /// This is invoked when the client is started.
    /// </summary>
    public override void OnStartClient()
    {
    }

    /// <summary>
    /// This is called when a host is stopped.
    /// </summary>
    public override void OnStopHost()
    {
    }

    /// <summary>
    /// This is called when a server is stopped - including when a host is stopped.
    /// </summary>
    public override void OnStopServer()
    {
    }

    /// <summary>
    /// This is called when a client is stopped.
    /// </summary>
    public override void OnStopClient()
    {
    }

    #endregion

    private void AddPlayerToSpawnedList(GameObject player)
    {
        var isPlayerScriptExists = player.TryGetComponent<Player>(out var playerScript);
        if (!isPlayerScriptExists)
        {
            Debug.LogAssertion("Player script not attached to GameObject.");
            return;
        }
        
        SpawnedPlayers.Add(playerScript);
    }
}