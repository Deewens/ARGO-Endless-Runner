using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Net;
using UnityEngine.Networking;
using TMPro;

public class LoginData
{
    public string username= " ";
    public string password = " ";
}

public class RegisterLoginManagement : MonoBehaviour
{
    LoginData loginData = new LoginData();

    public List<string> ExistingUsernames;
    public string[] ExistingPasswords;

    public TMPro.TMP_InputField usernameInput;
    public TMPro.TMP_InputField passwordInput;
    //public Button registerButton;
    //public Button goToLoginButton;

    ArrayList credentials;

    // Start is called before the first frame update
    void Start()
    {
        
        //registerButton.onClick.AddListener(writeStuffToFile);
        //goToLoginButton.onClick.AddListener(goToLoginScene);

        //if (File.Exists(Application.dataPath + "/credentials.txt"))
        //{
        //    credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        //}
        //else
        //{
        //    File.WriteAllText(Application.dataPath + "/credentials.txt", "");
        //}

    }

    void goToLoginScene()
    {
        SceneManager.LoadScene("Login");
    }


    void writeStuffToFile()
    {
        //bool isExists = false;

        //credentials = new ArrayList(File.ReadAllLines(Application.dataPath + "/credentials.txt"));
        //foreach (var i in credentials)
        //{
        //    if (i.ToString().Contains(usernameInput.text))
        //    {
        //        isExists = true;
        //        break;
        //    }
        //}

        //if (isExists)
        //{
        //    Debug.Log($"Username '{usernameInput.text}' already exists");
        //}
        //else
        //{
        //    credentials.Add(usernameInput.text + ":" + passwordInput.text);
        //    File.WriteAllLines(Application.dataPath + "/credentials.txt", (String[])credentials.ToArray(typeof(string)));
        //    Debug.Log("Account Registered");
        //}
    }

    public void clearFields()
    {
        usernameInput.text = "";
        passwordInput.text = "";
    }

    public void sendData()
    {
        Debug.Log("Sending Data");


        loginData.username = usernameInput.text;
        loginData.password = passwordInput.text;
        clearFields();
        string jsonData = JsonUtility.ToJson(loginData);
        StartCoroutine(PostMethod(jsonData));


    }

    public void GetData()
    {
        Debug.Log("getting Data");
        

        loginData.username = usernameInput.text;
        loginData.password = passwordInput.text;
        clearFields();
        string jsonData = JsonUtility.ToJson(loginData);
        StartCoroutine(GetUsername());


    }

    public IEnumerator GetUsername()
    {
        
        string usernameURL = "https://TQLOBBSN2N5PMVQY.anvil.app/IANHMSZIEXYQHRVG3CB6WIA4/_/api/usernames";

        UnityWebRequest request = UnityWebRequest.Get(usernameURL);
        

            yield return request.SendWebRequest();
            

            if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
            {
            ExistingUsernames.Add(request.downloadHandler.text);
            Debug.Log(ExistingUsernames[0]);

            Debug.Log("Data successfully received from the server");

            }
            else
            {
                Debug.Log("Error receiving data from the server: Error " + request.responseCode);
            }
        
    }

    /// <summary>
    /// Sends the data to an external database hosted on anvil
    /// </summary>
    public static IEnumerator PostMethod(string jsonData)
    {
        string url = "https://TQLOBBSN2N5PMVQY.anvil.app/IANHMSZIEXYQHRVG3CB6WIA4/_/api/credentials";
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