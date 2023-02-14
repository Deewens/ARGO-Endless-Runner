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
    public Image TogglePasswordCensorIcon;
    public Sprite Locked;
    public Sprite unlocked;
    public TMPro.TextMeshProUGUI UsernameHud;

    public TMPro.TMP_InputField usernameInput;
    public TMPro.TMP_InputField passwordInput;
    private bool ValidPassword;
    private bool ValidUsername;
    private bool LoggedIn;
    private string URLGameServer;
    private string PathUpdateCredentials;
    private string PathUpdateFeedback;
    private string PathCheckUserName;
    private string PathCheckUsernameAndPassword;
    private string jsonData;
    public string LoggedInUser;

    // Start is called before the first frame update
    void Start()
    {
        LoggedIn = false;
        URLGameServer = "https://TQLOBBSN2N5PMVQY.anvil.app/IANHMSZIEXYQHRVG3CB6WIA4/_/api/";
        PathUpdateCredentials = "credentials";
        PathUpdateFeedback = "playtestdata";
        PathCheckUserName = "checkusername";
        PathCheckUsernameAndPassword = "checkusernameandpassword";

        passwordInput.contentType = TMPro.TMP_InputField.ContentType.Password;
        TogglePasswordCensorIcon.sprite = Locked;

        ValidPassword = false;
        ValidUsername = false;
    }

    public void clearFields()
    {
        usernameInput.text = "";
        passwordInput.text = "";
        ValidPassword = false;
        ValidUsername = false;
    }

    public void GoToMainMenu()
    {
        this.gameObject.SetActive(false);
    }

    public void CensorText()
    {
       // Debug.Log("Function Called");
        if (passwordInput.contentType == TMPro.TMP_InputField.ContentType.Password)
        {
          //  Debug.Log("Standard");
            TogglePasswordCensorIcon.sprite = unlocked;
            passwordInput.contentType = TMPro.TMP_InputField.ContentType.Standard;
        }
        else
        {
           // Debug.Log("Password");
            TogglePasswordCensorIcon.sprite = Locked;
            passwordInput.contentType = TMPro.TMP_InputField.ContentType.Password;
        }
    }

    public void RemoveSpaces()
    {
        //Debug.Log("With Spaces : " + usernameInput.text);
        //Debug.Log("With Spaces : " + passwordInput.text);
        usernameInput.text = usernameInput.text.Replace(" ", "");
        passwordInput.text = passwordInput.text.Replace(" ", "");
        //Debug.Log("Without Spaces : " + usernameInput.text);
        //Debug.Log("Without Spaces : " + passwordInput.text);
    }

    public void Login()
    {
        Debug.Log("getting Data");
        RemoveSpaces();
        if (passwordInput.text.Length >= 6 && usernameInput.text.Length >= 6)
        {
            loginData.username = usernameInput.text;
            loginData.password = passwordInput.text;
            clearFields();
            jsonData = JsonUtility.ToJson(loginData);
            StartCoroutine(LogInAccount(jsonData));
        }
        else
        {
           // Debug.Log("Passwords and Usernames need to be at least 6 characters long");
        }
    }

    public IEnumerator LogInAccount(string jsonData)
    {
        string url = URLGameServer + PathCheckUsernameAndPassword;

        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (request.downloadHandler.text == "CanLogIn")
            {
                //Debug.Log("Can log in valid details entered");
                UsernameHud.text = loginData.username;
                LoggedIn = true;
                GoToMainMenu();
            }
            else
            {
               // Debug.Log("Can't log in invalid details entered");
            }

            if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
            {
                //Debug.Log("Downloaded : " + request.downloadHandler.text);
                //Debug.Log("Data successfully sent to the server");

            }
            else
            {
               // Debug.Log("Error sending data to the server: Error " + request.responseCode);
            }
        }
    }

    public void RegisterData()
    {
        //Debug.Log("getting Data");
        RemoveSpaces();
        if (passwordInput.text.Length >= 6 && usernameInput.text.Length >=6)
        {
            loginData.username = usernameInput.text;
            loginData.password = passwordInput.text;
            clearFields();
            jsonData = JsonUtility.ToJson(loginData);
            StartCoroutine(RegisterUsername(jsonData));
        }
        else
        {
            //Debug.Log("Passwords and Usernames need to be at least 6 characters long");
        }
        // StartCoroutine(GetUsername());
    }

    //public IEnumerator ValidatePassword(string jsonData)
    //{
    //    string url = URLGameServer + PathCheckPassword;
    //   // Debug.Log(jsonData);
    //    using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
    //    {
    //        request.method = UnityWebRequest.kHttpVerbPOST;
    //        request.SetRequestHeader("Content-Type", "application/json");
    //        request.SetRequestHeader("Accept", "application/json");

    //        yield return request.SendWebRequest();

    //        if(request.downloadHandler.text == "true")
    //        {
    //            ValidPassword = true;
    //        }
    //        else
    //        {
    //            ValidPassword = false;
    //        }


    //        if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
    //        {
    //            Debug.Log("Downloaded : " + request.downloadHandler.text);
    //            Debug.Log("Data successfully sent to the server");

    //        }
    //        else
    //        {
    //            Debug.Log("Error sending data to the server: Error " + request.responseCode);
    //        }
    //    }
    //}

    public IEnumerator RegisterUsername(string jsonData)
    {
        string url = URLGameServer + PathCheckUserName;

        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (request.downloadHandler.text == "NotTaken")
            {
                StartCoroutine(PostMethod(jsonData, PathUpdateCredentials));
            }
            else
            {
                ValidUsername = false;
            }

            if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
            {
                //Debug.Log("Downloaded : " + request.downloadHandler.text);
               // Debug.Log("Data successfully sent to the server");

            }
            else
            {
                //Debug.Log("Error sending data to the server: Error " + request.responseCode);
            }
        }
    }

    /// <summary>
    /// Sends the data to an external database hosted on anvil
    /// </summary>
    public IEnumerator PostMethod(string jsonData, string t_path)
    {
        // player data and credentials
        string url = "https://TQLOBBSN2N5PMVQY.anvil.app/IANHMSZIEXYQHRVG3CB6WIA4/_/api/credentials";

        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
            {
                //Debug.Log("Data Registered");

            }
            else
            {
                //Debug.Log("Error sending data to the server: Error " + request.responseCode);
            }
        }
    }

}