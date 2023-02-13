/// Author : Patrick Donnelly
/// Contributors : Izzy - RatingForm
 
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// A class which adds functionality to main menu buttons,game over buttons, and the 'Auto' game button
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Scenes (scene added here must be in build settings)")]
    [Scene] 
    [SerializeField] 
    private string mainMenuScene = "";

    public GameObject GameOverMenu;
    public Button _toggleAIButton;
    
    private bool _isRunnerAIOn = true;
    private bool _isGodAIOn = true;
    
    private GameObject _runner;
    public GameObject Runner
    {
        set => _runner = value;
    }

    /// <summary>
    /// Loads the MainMenu upon button press 
    /// </summary>
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
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
    public void ToggleRunnerAIButton()
    {
        //Debug.Log("here at toggle");
        // _toggleAIButton.text = "Hi";
        if (!_isRunnerAIOn)
        {
            _isRunnerAIOn = true;
            _toggleAIButton.image.color = Color.red;
            _runner.transform.GetChild(1).gameObject.SetActive(true);
            _runner.GetComponent<AI_Brain>().enabled = true;
            _runner.GetComponent<RunnerPlayer>().enabled = false;
        }
        else
        {
            _isRunnerAIOn = false;
            _toggleAIButton.image.color = Color.white;
            _runner.transform.GetChild(1).gameObject.SetActive(false);
            _runner.GetComponent<AI_Brain>().enabled = false;
            _runner.GetComponent<RunnerPlayer>().enabled = true;
        }
    }

    public void ToggleGodAIButton()
    {
        if (!_isGodAIOn)
        {
        }
        else
        {

        }
    }
}