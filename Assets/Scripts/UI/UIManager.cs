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

using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
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

    [FormerlySerializedAs("GameOverMenu")] public GameObject gameOverMenu;
    [FormerlySerializedAs("_toggleAIButton")] public Button toggleAIButton;
    
    private bool _isRunnerAIOn = false;
    private bool _isGodAIOn = false;
    
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
        gameOverMenu.SetActive(true);
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
            toggleAIButton.image.color = Color.red;
            _runner.transform.GetChild(1).gameObject.SetActive(true);
            _runner.GetComponent<AIBrain>().enabled = true;
            _runner.GetComponent<RunnerPlayer>().enabled = false;
        }
        else
        {
            _isRunnerAIOn = false;
            toggleAIButton.image.color = Color.white;
            _runner.transform.GetChild(1).gameObject.SetActive(false);
            _runner.GetComponent<AIBrain>().enabled = false;
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