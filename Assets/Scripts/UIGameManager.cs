using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    private int _chosenAttack = -1;

    public void chooseAvoid()
    {
        _chosenAttack = 0;
    }

    public int GetChosenAttack()
    {
        return _chosenAttack;
    }
}