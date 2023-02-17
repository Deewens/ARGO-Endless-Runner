/*
 Authors:
    - Patrick Donnelly
    - Izzy - RatingForm,
    - Adrien Dudon 
*/

using Mirror;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// A class which adds functionality to main menu buttons,game over buttons, and the 'Auto' game button
/// </summary>
public class MenuManager : MonoBehaviour
{
    [Header("Scenes (MUST be added to build settings)")]
    [Scene] 
    [SerializeField] 
    private string mainMenuScene = "";
    
    [Scene] 
    [SerializeField] 
    private string multiplayerMenuScene = "";
    
    [Scene]
    [SerializeField] 
    private string singleplayerMainScene = "";

    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject leaderboardPanel;

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
        Debug.Log("Button deactivated for now.");
    }

    /// <summary>
    /// Loads the MainScene upon button press
    /// </summary>
    public void LoadSingleplayerScene()
    {
        SceneManager.LoadScene(singleplayerMainScene);
    }
    
    public void LoadMultiplayerMenuScene()
    {
        SceneManager.LoadScene(multiplayerMenuScene);
    }

    public void DisplayLeaderboard()
    {
        GameObject title = mainMenuPanel.transform.GetChild(0).transform.Find("Title").gameObject;
        if (title != null)
        {
            title.SetActive(false);
        }
        
        foreach (Transform childTransform in mainMenuPanel.transform)
        {
            if (childTransform.GetComponent<Button>() != null)
            {
                childTransform.gameObject.SetActive(false);
            }
        }
        
        leaderboardPanel.SetActive(true);
    }

    public void HideLeaderboard()
    {
        GameObject title = mainMenuPanel.transform.GetChild(0).transform.Find("Title").gameObject;
        if (title != null)
        {
            title.SetActive(true);
        }
        
        foreach (Transform childTransform in mainMenuPanel.transform)
        {
            if (childTransform.GetComponent<Button>() != null)
            {
                childTransform.gameObject.SetActive(true);
            }
        }
        
        leaderboardPanel.SetActive(false);
    }

    /// <summary>
    /// Quits game upon button press
    /// </summary>
    public void QuitGame()
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
        SceneManager.LoadScene(mainMenuScene);
    }
}
