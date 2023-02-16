using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    public TextAsset textAssetData;
    private NeuralNetwork _brain;

    void Awake()
    {
        _brain = GetComponent<NeuralNetwork>();
    }

    public void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { "\r\n" }, System.StringSplitOptions.None);
        string[] line;

       for (int i = 0; i < 6;i++)
       {
            line = data[i].ToString().Split(",");

            for(int j =0; j<line.Length;j++)
            {
                _brain.SetWeightsLayer1(i, j, float.Parse(line[j]));
            }
       }
       int index = 0;
       for(int i = 6; i < 11;i++)
       {
            line = data[i].ToString().Split("\r");

            _brain.SetBias(index, float.Parse(line[0]));
            index++;
       }
       index = 0;

       for(int i = 11; i < 16;i++)
       {
            line = data[i].ToString().Split(",");

            _brain.SetWeightsLayer2(index, float.Parse(line[0]));

            //for (int j = 0; j < line.Length; j++)
            //{

            //}
            index++;
        }


        line = data[16].ToString().Split("\r");
        _brain.SetBias(5, float.Parse(line[0]));
    }
}
