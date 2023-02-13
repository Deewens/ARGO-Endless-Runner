//Author : Izabela Zelek, February 2023
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// A class which saves data to CSV file
/// </summary>
public class CSVWriter : MonoBehaviour
{

    private string _filename = "";

    // Start is called before the first frame update
    void Start()
    {
        _filename = Application.dataPath + "/training_data.csv";
    }


    public void WriteCSV(float obsX, float obsY,float playerX,float playerY)
    {
        TextWriter tw = new StreamWriter(_filename, true);

        Debug.Log(obsX + "," + obsY + "," + playerX + "," + playerY);
        tw.WriteLine(obsX + "," + obsY + "," + playerX + "," + playerY);
        tw.Close();
    }
}
