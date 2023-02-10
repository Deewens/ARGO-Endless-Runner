using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameObject _runner;
    public Button _toggleAIButton;
    bool _aiOnOff;

    private void Start()
    {
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

    public void ToggleAIButton()
    {
        // _toggleAIButton.text = "Hi";
        if (!_aiOnOff)
        {
            _aiOnOff = true;
            _toggleAIButton.image.color = Color.red;
            _runner.transform.GetChild(1).gameObject.SetActive(true);
            _runner.GetComponent<AI_Brain>().enabled = true;
            _runner.GetComponent<RunnerPlayer>().enabled = false;

        }
        else
        {
            _aiOnOff = false;
            _toggleAIButton.image.color = Color.white;
            _runner.transform.GetChild(1).gameObject.SetActive(false);
            _runner.GetComponent<AI_Brain>().enabled = false;
            _runner.GetComponent<RunnerPlayer>().enabled = true;


        }
    }


}
