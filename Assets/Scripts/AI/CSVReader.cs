using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    public TextAsset textAssetData;

    void Update()
    {
        ReadCSV();
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, System.StringSplitOptions.None);

        int tableSize = 4;

        for(int i = 0; i < tableSize; i++)
        {
            Debug.Log(data);
        }
    }
}
