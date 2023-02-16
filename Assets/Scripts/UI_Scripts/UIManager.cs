//Author : Izabela Zelek, February 2023
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// A class which adds functionality to main menu buttons,game over buttons, and the 'Auto' game button
/// </summary>
public class UIManager : MonoBehaviour
{
    private string CurrentScene;
    private Camera runnerCam;
    private Camera godCam;

    private GameObject godCanvas;
    private GameObject runnerCanvas;

    public GameObject GameOverMenu;
    public GameObject Runner;
    public Button ToggleAIButton;
    public Button CaptureButton;
    private bool _aiOnOff;
    //private bool _capture = false;

    private void Start()
    {
        CurrentScene = SceneManager.GetActiveScene().name;
        if (CurrentScene != "MainMenu")
        {
            runnerCam = GameObject.Find("Runner Camera").GetComponent<Camera>();
            godCam = GameObject.Find("God Camera").GetComponent<Camera>();

            godCanvas = GameObject.Find("God Canvas");
            runnerCanvas = GameObject.Find("Runner Canvas");

            godCanvas.SetActive(false);
            godCam.enabled = false;
        }

        //_toggleAIButton = _gameplayCanvas.GetComponentInChildren<Button>();
    }

    /// <summary>
    /// Upon clicking, opens rating form in browser
    /// </summary>
    /// 
    public void GoToFormRatingForm()
    {
        Application.OpenURL(
            "https://docs.google.com/forms/d/e/1FAIpQLScP_TMz7UApxi89AMviheraT5H10M79u2_8E2bpfZZvgiWpZw/viewform?usp=sf_link");
    }

    /// <summary>
    /// Loads the MainScene again upon button press
    /// </summary>
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Loads the MainScene upon button press
    /// </summary>
    public void StartLevel()
    {
        SceneManager.LoadScene("MainScene");
    }

    /// <summary>
    /// Quits game upon button press
    /// </summary>
    public void Quitgame()
    {
        #if UNITY_STANDALONE
          Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }

    /// <summary>
    /// Loads the MainMenu upon button press 
    /// </summary>
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Toggles the gameover screen
    /// </summary>
    private void OnEnable()
    {
        ObstacleCollision.OnPlayerDeath += EnableGameOvermenu;
    }

    /// <summary>
    /// Toggle sthe game over screen
    /// </summary>
    private void OnDisable()
    {
        ObstacleCollision.OnPlayerDeath -= EnableGameOvermenu;
    }

    public void EnableGameOvermenu()
    {
        GameOverMenu.SetActive(true);
    }

    /// <summary>
    /// Toggles AI off and on upon button press
    /// </summary>
    public void ToggleAI()
    {
        //Debug.Log("here at toggle");
        // _toggleAIButton.text = "Hi";
        if (!_aiOnOff)
        {
            _aiOnOff = true;
            ToggleAIButton.image.color = Color.red;
            Runner.transform.GetChild(1).gameObject.SetActive(true);
            Runner.GetComponent<AI_Brain>().enabled = true;
            Runner.GetComponent<RunnerPlayer>().enabled = false;

            godCanvas.SetActive(true);
            godCam.enabled = true;
            runnerCanvas.SetActive(false);
            runnerCam.enabled = false;
        }
        else
        {
            _aiOnOff = false;
            ToggleAIButton.image.color = Color.white;
            Runner.transform.GetChild(1).gameObject.SetActive(false);
            Runner.GetComponent<AI_Brain>().enabled = false;
            Runner.GetComponent<RunnerPlayer>().enabled = true;

            godCanvas.SetActive(false);
            godCam.enabled = false;
            runnerCanvas.SetActive(true);
            runnerCam.enabled = true;
        }
    }

    public void ToggleCapture()
    {
        Runner.GetComponent<RunnerPlayer>().TurnOnCapture();

        if(Runner.GetComponent<RunnerPlayer>().IsCapture())
        {
            ToggleAIButton.image.color = Color.yellow;
        }
        else
        {
            ToggleAIButton.image.color = Color.white;
        }
      
    }
}
