/*
Olympus Run - A game made as part of the ARGO Project at SETU Carlow
Copyright (C) 2023 Caroline Percy <lineypercy@me.com>, Patrick Donnelly <patrickdonnelly3759@gmail.com>, Izabela Zelek <C00247865@itcarlow.ie>, Danial-hakim <danialhakim01@gmail.com>, Adrien Dudon <dudonadrien@gmail.com>

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
using TMPro;
using UnityEngine;

public class Score : NetworkBehaviour
{
    private TMPro.TextMeshProUGUI _comboText;
    private GameObject _comboCanvas;
    private bool _timerIsRunning;
    private float _comboTimeRemaining;
    private int _totalComboPointsSoFar;
    private int _currentCombo;

    private TMPro.TextMeshProUGUI _bonusPointsText;
    private GameObject _bonusPointsCanvas;
    private int _baseIncrease;
    private int _currentPlayTime;
    private int _currentDistanceTravelled;
    private int _currentSpeed;
    private int _totalBonusPointsSoFar;
    private float _popUpTimeRemaining;
    private bool _popUpTimerIsRunning;
    private bool _distanceMissionComplete;
    private bool _speedMissionComplete;
    private bool _timeAliveMissionComplete;

    private TMPro.TextMeshProUGUI _scoreText;
    private GameObject _scoreCanvas;
    private int _totalScore;

    private MoveForward _moveForwardScript;


    private void Start()
    {
        _comboCanvas = GameObject.Find("ComboCanvas");
        _bonusPointsCanvas = GameObject.Find("BonusPointsCanvas");
        _scoreCanvas = GameObject.Find("ScoreCanvas");

        if (!isLocalPlayer)
        {
            // Deactivate every UI if you're not the local player
            _comboCanvas.SetActive(false);
            _bonusPointsCanvas.SetActive(false);
            _scoreCanvas.SetActive(false);
            return;
        }
        
        _distanceMissionComplete = false;
        _speedMissionComplete = false;
        _timeAliveMissionComplete = false;
        _totalScore = 0;
        _baseIncrease = 50;
        _currentCombo = 0;
        _totalComboPointsSoFar = 0;
        _comboTimeRemaining = 3.0f;
        _comboText = _comboCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _comboCanvas.SetActive(false);

        _popUpTimeRemaining = 3.0f;
        _bonusPointsText = _bonusPointsCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        _bonusPointsCanvas.SetActive(false);

        _scoreText = _scoreCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        _moveForwardScript = GetComponent<MoveForward>();
        _currentPlayTime = 0;
        _currentSpeed = 0;
    }
    // Start is called before the first frame update

    void Update()
    {
        // Do not perform any calculation related to the score if you're not the local player, because you already doing it in the LocalPlayer game instance
        if (!isLocalPlayer)
            return;
        
        if(_comboText == null || _comboCanvas == null)
        {
            _comboCanvas = GameObject.Find("ComboCanvas");
            _comboText = _comboCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _comboCanvas.SetActive(false);
        }
        if (_bonusPointsText == null || _bonusPointsCanvas == null)
        {
            Debug.Log("Cannot Find BonusPointsCanvas - null refernce");
            _bonusPointsCanvas = GameObject.Find("BonusPointsCanvas");
            _bonusPointsText = _bonusPointsCanvas.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
            _bonusPointsCanvas.SetActive(false);
        }
        if (_scoreText == null || _scoreCanvas == null)
        {
            Debug.Log("Cannot Find ScoreCanvas - null refernce");
            _scoreCanvas = GameObject.Find("ScoreCanvas");
            _scoreText = _scoreCanvas.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
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
            }
        }
        if (_popUpTimerIsRunning)
        {
            if (_popUpTimeRemaining > 0)
            {
                _popUpTimeRemaining -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Combo Finished!");
                ResetPopUp();
            }
        }

        //Debug.Log(_moveForwardScript.GetPlayTime());

        CheckDistanceBonusPoints();
        CheckSpeedBonusPoints();
        CheckTimeAliveBonusPoints();
    }

    private void CheckSpeedBonusPoints()
    {
        if (CheckIfSpeedChanged())
        {
            if (_moveForwardScript.GetSpeed() < 24)
            {
                if (_moveForwardScript.GetSpeed() % 4 == 0 && _moveForwardScript.GetSpeed() > 8)
                {
                    _speedMissionComplete = true;
                    _baseIncrease = 50;
                    AddMissionPoints(_moveForwardScript.GetSpeed() / 4);
                }
            }
            else if (_moveForwardScript.GetSpeed() == 24)
            {
                _speedMissionComplete = true;
                _baseIncrease = 100;
                AddMissionPoints(_moveForwardScript.GetSpeed() / 6);
            }
            else if (_moveForwardScript.GetSpeed() == 30)
            {
                _speedMissionComplete = true;
                _baseIncrease = 100;
                AddMissionPoints(_moveForwardScript.GetSpeed() / 5);
            }
        }
    }

    /// <summary>
    /// Ensures Bonus points for Distance Travelled Missions are only applied
    /// once and not multiple times
    /// </summary>
    /// <returns></returns>
    private bool CheckIfDistanceTravelledChanged()
    {
        if (_currentDistanceTravelled < _moveForwardScript.GetDistanceTravelled())
        {
            _currentDistanceTravelled = _moveForwardScript.GetDistanceTravelled();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Ensures Bonus points for Speed Missions are only applied
    /// once and not multiple times
    /// </summary>
    /// <returns></returns>
    private bool CheckIfSpeedChanged()
    {
        if (_currentSpeed < _moveForwardScript.GetSpeed())
        {
            _currentSpeed = _moveForwardScript.GetSpeed();
            return true;
        }
        return false;
    }

    /// <summary>
    /// Ensures Bonus points for Play Time Missions are only applied
    /// once and not multiple times
    /// </summary>
    /// <returns></returns>
    private bool CheckIfPlayTimeChanged()
    {
        if (_currentPlayTime < _moveForwardScript.GetPlayTime())
        {
            _currentPlayTime = _moveForwardScript.GetPlayTime();
            return true;
        }
        return false;
    }

    private void CheckTimeAliveBonusPoints()
    {
        if (CheckIfPlayTimeChanged())
        {
            if (_moveForwardScript.GetPlayTime() >=30)
            {
                if (_moveForwardScript.GetPlayTime() == 30)
                {
                    _timeAliveMissionComplete = true;
                    _baseIncrease = 100;
                    AddMissionPoints(_moveForwardScript.GetPlayTime() / 15);
                }

                else if (_moveForwardScript.GetPlayTime() == 60)
                {
                    _timeAliveMissionComplete = true;
                    _baseIncrease = 100;
                    AddMissionPoints(_moveForwardScript.GetPlayTime() / 12);
                }
                else if (_moveForwardScript.GetPlayTime() % 60 == 0)
                {
                    _timeAliveMissionComplete = true;
                    _baseIncrease = 100;
                    AddMissionPoints(_moveForwardScript.GetPlayTime() / 12);
                }
            }
        }

    }

    private void ResetPopUp()
    {
        _popUpTimeRemaining = 0;
        _bonusPointsText.text = "";
        _totalBonusPointsSoFar = 0;
        _popUpTimerIsRunning = false;
        _bonusPointsCanvas.SetActive(false);
        _distanceMissionComplete = false;
        _speedMissionComplete = false;
        _timeAliveMissionComplete = false;
    }

    private void ResetCombo()
    {
        _currentCombo = 0;
        _totalComboPointsSoFar = 0;
        _comboText.text = "";
        _comboTimeRemaining = 0;
        _timerIsRunning = false;
        _comboCanvas.SetActive(false);
    }

    public void CombineScore(int DistanceTravelled)
    {
        _totalScore+= DistanceTravelled;
    }

    public void AddMissionPoints(int Multiplier)
    {
        _totalScore += _baseIncrease * Multiplier;
        SetBonusPointsText(_baseIncrease * Multiplier);
    }

    public void AddComboPoints(int CollectiblePoints)
    {
        SetComboTimer();
        _currentCombo += 1;
        _totalComboPointsSoFar += CollectiblePoints * _currentCombo;
        _totalScore += CollectiblePoints * _currentCombo;
        SetComboText(_currentCombo);
        _scoreText.text = "" + _totalScore + "";
        //Debug.Log("Combo : " + _currentCombo);
        //Debug.Log("Combo Points : " + _totalComboPointsSoFar);
        //Debug.Log("Score : " + _totalScore);

    }

    private void SetComboTimer()
    {
        _comboTimeRemaining = 3.0f;
        _timerIsRunning = true;
    }

    private void SetComboText(int CurrentCombo)
    {
        _comboCanvas.SetActive(true);
        _comboText.text = "COMBO X " + CurrentCombo + " : " + _totalComboPointsSoFar + " Points";
    }

    private void SetPopUpTimer()
    {
        _popUpTimeRemaining = 3.0f;
        _popUpTimerIsRunning = true;
    }

    private void SetBonusPointsText(int BonusPoints)
    {
        _totalScore += BonusPoints;
        _totalBonusPointsSoFar += BonusPoints;
        _bonusPointsText.text = "";
        SetPopUpTimer();
        _bonusPointsCanvas.SetActive(true);
        if (_distanceMissionComplete)
        {
            _bonusPointsText.text += ("Distance Travelled " + _moveForwardScript.GetDistanceTravelled() + "\r\n");
        }
        if (_speedMissionComplete)
        {
            _bonusPointsText.text += ("Speed Reached " + _moveForwardScript.GetSpeed() + "\r\n");
        }
        if (_timeAliveMissionComplete)
        {
            _bonusPointsText.text += ("Seconds Survived " + _moveForwardScript.GetPlayTime() + "\r\n");
        }
        _bonusPointsText.text += "+" + _totalBonusPointsSoFar + " Points";
        _scoreText.text = "" + _totalScore + "";
    }

    private void CheckDistanceBonusPoints()
    {
        if (CheckIfDistanceTravelledChanged())
        {
            if (_moveForwardScript.GetDistanceTravelled() > 0)
            {
                if (_moveForwardScript.GetDistanceTravelled() == 100)
                {
                    _distanceMissionComplete = true;
                    _baseIncrease = 50;
                    AddMissionPoints(_moveForwardScript.GetDistanceTravelled() / 100);
                }
                else if (_moveForwardScript.GetDistanceTravelled() % 250 == 0 && _moveForwardScript.GetDistanceTravelled() <= 1000)
                {
                    _distanceMissionComplete = true;
                    _baseIncrease = 50;
                    AddMissionPoints(_moveForwardScript.GetDistanceTravelled() / 125);
                }
                else if (_moveForwardScript.GetDistanceTravelled() % 500 == 0)
                {
                    _distanceMissionComplete = true;
                    _baseIncrease = 100;
                    AddMissionPoints(_moveForwardScript.GetDistanceTravelled() / 250);
                }
            }
        }
    }
}
