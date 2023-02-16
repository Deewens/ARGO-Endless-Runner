/// Author : Patrick Donnelly
/// Contributors : Izzy - RatingForm

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// A class which adds functionality to main menu buttons,game over buttons, and the 'Auto' game button
/// </summary>
public class MenuManager : MonoBehaviour
{
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
}
