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

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalController : MonoBehaviour
{
    private List<string> _goals = new List<string>();
    private int _chosenGoal = -1;
    private int _lastGoal = -1;
    private int _maxGoal = 7;
    private TextMeshProUGUI _goalText;
    private bool _goalComplete = false;

    private int _appleCount = 0;
    private int _pomCount = 0;
    private int _speedUpCount = 0;
    private int _speedDownCount = 0;

    private int _twenty = 5;
    private int _fiveteen = 15;
    private int _ten = 10;
    private int _five = 5;
    private int _one = 5;
    private int _two = 5;


    private bool _healthGoalComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        _goalText = GameObject.Find("Goal").GetComponent<TextMeshProUGUI>();
        AddGoals();

        _chosenGoal = Random.Range(0, _maxGoal);

        _lastGoal = _chosenGoal;
        _goalText.text = _goals[_chosenGoal];

    }

    // Update is called once per frame
    void Update()
    {
        switch(_chosenGoal)
        {
            case 0:
                if(_appleCount >= 10)
                {
                    _appleCount = 0;
                    _goalComplete = true;
                }
                break;
            case 1:
                if (_appleCount >= 20)
                {
                    _appleCount = 0;
                    _goalComplete = true;
                }
                break;
            case 2:
                if (_pomCount >= 15)
                {
                    _pomCount = 0;
                    _goalComplete = true;
                }
                break;
            case 3:
                if (_pomCount >= 5)
                {
                    _pomCount = 0;
                    _goalComplete = true;
                }
                break;
            case 4:
                if (_speedUpCount >= 2)
                {
                    _speedUpCount = 0;
                    _goalComplete = true;
                }
                break;
            case 5:
                if (_speedDownCount >= 1)
                {
                    _speedDownCount = 0;
                    _goalComplete = true;
                }
                break;
            case 6:
                if(_healthGoalComplete)
                {
                    _healthGoalComplete = false;
                    _goalComplete = true;
                }
                break;
        }

        if(_goalComplete)
        {
            _chosenGoal = Random.Range(0, _maxGoal);
            while(_lastGoal == _chosenGoal)
            {
                _chosenGoal = Random.Range(0, _maxGoal);
            }

            _goalText.text = _goals[_chosenGoal];

            _goalComplete = false;
            _lastGoal = _chosenGoal;
        }
    }

    private void AddGoals()
    {
        _goals.Add("Collect 10 apples");
        _goals.Add("Collect 20 apples");
        _goals.Add("Collect 15 pomegranates");
        _goals.Add("Collect 5 pomegranates");
        _goals.Add("Use 2 SpeedUp Potions");
        _goals.Add("Use 1 SpeedDown Potion");
        _goals.Add("Use MaxHealth Potion on full health");
    }

    public void AddApple()
    {
        if (_chosenGoal == 0 || _chosenGoal == 1)
        { 
            _appleCount++; 
        }
    }
    public void AddPom()
    {
        if (_chosenGoal == 2 || _chosenGoal == 3)
        {
            _pomCount++;
        }
    }
    public void AddSpeedUp()
    {
        if(_chosenGoal == 4)
        {
            _speedUpCount++;
        }
    }
    public void AddSpeedDown()
    {
        if (_chosenGoal == 5)
        {
            _speedDownCount++;
        }
    }

     public void CheckHealth()
    {
        if (_chosenGoal == 6)
        {
            _healthGoalComplete = true;
        }
    }
}
