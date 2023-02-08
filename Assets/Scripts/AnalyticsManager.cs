using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class Data
{
    public int playerID = 0;
    public string role = " ";
    public int distanceTravelled = 0;
    public int obstaclesAvoided = 0;
    public int obstaclesHit = 0;
    public int obstaclesPlaced = 0;
    public int score = 0;
    public int timePlayed = 0;
}

public class AnalyticsManager : MonoBehaviour
{
    public GameObject _runner;
    public GameObject _godZeus;
    public GameObject _godPoseidon;


    Data _runnerData = new Data(); //initialise all data
    Data _zeusData = new Data(); //initialise all data
    Data _poseidonData = new Data(); //initialise all data


    void Start()
    {
        _runnerData.playerID = Random.Range(1, 100000000);
        _zeusData.playerID = Random.Range(1, 100000000);
        _poseidonData.playerID = Random.Range(1, 100000000);
    }


    public void sendData()
    {
        Debug.Log("Here");
        if (_runner.tag == "Runner")
        {
            _runnerData.role = "Runner";
            _runnerData.distanceTravelled = _runner.GetComponent<MoveForward>()._distanceTravelled;
            // extract data from finished scripts for the runner later on 
            _runnerData.obstaclesAvoided = 0;
            _runnerData.obstaclesHit = 0;
            _runnerData.score = 0;
            _runnerData.timePlayed = 0;

            string jsonData = JsonUtility.ToJson(_runnerData);
            StartCoroutine(PostMethod(jsonData));

        }
        if (_godZeus.tag == "Zeus")
        {
            // extract data from the zeus player when done
            _zeusData.role = "Zeus";
            _zeusData.obstaclesPlaced = 0;
            _zeusData.score = 0;
            _zeusData.timePlayed = 0;

            string jsonData = JsonUtility.ToJson(_zeusData);
            StartCoroutine(PostMethod(jsonData));
        }
        if (_godPoseidon.tag == "Poseidon")
        {
            // extract data from the poseidon player when done
            _poseidonData.role = "Poseidon";
            _poseidonData.obstaclesPlaced = 0;
            _poseidonData.score = 0;
            _poseidonData.timePlayed = 0;

            string jsonData = JsonUtility.ToJson(_poseidonData);
            StartCoroutine(PostMethod(jsonData));
        }
    }

    public static IEnumerator PostMethod(string jsonData)
    {
        string url = "https://TQLOBBSN2N5PMVQY.anvil.app/IANHMSZIEXYQHRVG3CB6WIA4/_/api/metric";
        //string url = "https://experienced-forceful-queen.anvil.app/IANHMSZIEXYQHRVG3CB6WIA4/_/api/metric";

        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
            {
                Debug.Log("Data successfully sent to the server");

            }
            else
            {
                Debug.Log("Error sending data to the server: Error " + request.responseCode);
            }
        }
    }
}
