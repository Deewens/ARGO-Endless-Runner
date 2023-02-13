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
    private Camera runnerCam;
    private Camera godCam;

    private GameObject godCanvas;
    private GameObject runnerCanvas;

    public GameObject GameOverMenu;
    public GameObject _runner;
    public Button _toggleAIButton;
    private bool _aiOnOff;

    private void Start()
    {
        runnerCam = GameObject.Find("Runner Camera").GetComponent<Camera>();
        godCam = GameObject.Find("God Camera").GetComponent<Camera>();

        godCanvas = GameObject.Find("God Canvas");
        runnerCanvas = GameObject.Find("Runner Canvas");

        godCanvas.SetActive(false);
        godCam.enabled = false;
        

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
    public void ToggleAIButton()
    {
        Debug.Log("here at toggle");
        // _toggleAIButton.text = "Hi";
        if (!_aiOnOff)
        {
            _aiOnOff = true;
            _toggleAIButton.image.color = Color.red;
            _runner.transform.GetChild(1).gameObject.SetActive(true);
            _runner.GetComponent<AI_Brain>().enabled = true;
            _runner.GetComponent<RunnerPlayer>().enabled = false;

            godCanvas.SetActive(true);
            godCam.enabled = true;
            runnerCanvas.SetActive(false);
            runnerCam.enabled = false;
        }
        else
        {
            _aiOnOff = false;
            _toggleAIButton.image.color = Color.white;
            _runner.transform.GetChild(1).gameObject.SetActive(false);
            _runner.GetComponent<AI_Brain>().enabled = false;
            _runner.GetComponent<RunnerPlayer>().enabled = true;

            godCanvas.SetActive(false);
            godCam.enabled = false;
            runnerCanvas.SetActive(true);
            runnerCam.enabled = true;
        }
    }


}
