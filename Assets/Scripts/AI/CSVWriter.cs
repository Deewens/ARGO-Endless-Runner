//Author : Izabela Zelek, February 2023
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// A class which saves data to CSV file
/// </summary>
public class CSVWriter : MonoBehaviour
{
    public GameObject parent;
    private string _filename = "";

    // Start is called before the first frame update
    void Start()
    {
        _filename = Application.dataPath + "/training_data.csv";
    }


    public void WriteCSV(float playerX,float playerY, float playerZ, float changedLane)
    {
        if (parent.transform.childCount > 0)
        {
            TextWriter tw = new StreamWriter(_filename, true);

            Debug.Log(parent.transform.GetChild(0).transform.position.x + "," 
                + parent.transform.GetChild(0).transform.position.y + "," 
                + Math.Round(parent.transform.GetChild(0).transform.position.z, 2, MidpointRounding.AwayFromZero) + ","
                + Math.Round(playerX, 2, MidpointRounding.AwayFromZero) + "," 
                + Math.Round(playerY, 2, MidpointRounding.AwayFromZero) + ","
                + Math.Round(playerZ, 2, MidpointRounding.AwayFromZero) + "," 
                + changedLane);

            tw.WriteLine(parent.transform.GetChild(0).transform.position.x + ","
                + parent.transform.GetChild(0).transform.position.y + ","
                + Math.Round(parent.transform.GetChild(0).transform.position.z, 2, MidpointRounding.AwayFromZero) + ","
                + Math.Round(playerX, 2, MidpointRounding.AwayFromZero) + ","
                + Math.Round(playerY, 2, MidpointRounding.AwayFromZero) + ","
                + Math.Round(playerZ, 2, MidpointRounding.AwayFromZero) + ","
                + changedLane);

            tw.Close();
        }
    }
}
