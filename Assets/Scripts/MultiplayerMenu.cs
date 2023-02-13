using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MultiplayerMenu : MonoBehaviour
{
    [Header("Scenes (MUST be added to build settings)")]
    [Scene] 
    [SerializeField] 
    private string mainMenuScene = "";
    
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void StartHostButton()
    {
        NetworkManager.singleton.StartHost();
    }
    
    public void StartClientButton()
    {
        NetworkManager.singleton.StartClient();
    }
}
