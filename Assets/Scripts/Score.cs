/// Author : Patrick Donnelly
/// Contributors : ---

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private TMPro.TextMeshProUGUI _comboText;
    private GameObject _comboCanvas;
    private int _totalScore;
    private int _baseIncrease;
    private int _currentCombo;
    private int _totalComboPointsSoFar;
    private float _comboTimeRemaining;
    private bool _timerIsRunning;

    // Start is called before the first frame update
    void Start()
    {
        _totalScore = 0;
        _baseIncrease = 50;
        _currentCombo = 0;
        _totalComboPointsSoFar = 0;
        _comboTimeRemaining = 3.0f;
        _comboCanvas = GameObject.Find("ComboCanvas");
        _comboText = _comboCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _comboCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(_comboText == null)
        {
            Debug.Log("Cannot Find ComboCanvas - null refernce");
            GameObject ComboCanvas = GameObject.Find("ComboCanvas");
            _comboText = ComboCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        if (_timerIsRunning)
        {
            if (_comboTimeRemaining > 0)
            {
                _comboTimeRemaining -= Time.deltaTime;
                //Debug.Log("Combo Running!");
            }
            else
            {
                Debug.Log("Combo Finished!");
                ResetCombo();
                _comboTimeRemaining = 0;
                _timerIsRunning = false;
            }
        }
    }

    private void ResetCombo()
    {
        _currentCombo = 0;
        _totalComboPointsSoFar = 0;
        _comboText.text = "";
        _comboCanvas.SetActive(false);
    }

    public void CombineScore(int DistanceTravelled)
    {
        _totalScore+= DistanceTravelled;
    }

    public void AddMissionPoints(int Multiplier)
    {
        _totalScore += _baseIncrease * Multiplier;
    }

    public void AddComboPoints(int CollectiblePoints)
    {
        SetComboTimer();
        _currentCombo += 1;
        SetComboText(_currentCombo);
        _totalComboPointsSoFar += CollectiblePoints * _currentCombo;
        _totalScore += CollectiblePoints * _currentCombo;
        Debug.Log("Combo : " + _currentCombo);
        Debug.Log("Combo Points : " + _totalComboPointsSoFar);
        Debug.Log("Score : " + _totalScore);

    }

    private void SetComboTimer()
    {
        _comboTimeRemaining = 3.0f;
        _timerIsRunning = true;
    }

    private void SetComboText(int CurrentCombo)
    {
        _comboCanvas.SetActive(true);
        _comboText.text = "COMBO X " + CurrentCombo;
    }
}
