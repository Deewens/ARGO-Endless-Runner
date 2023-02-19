﻿/*
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class LoginData
{
    public string username= " ";
    public string password = " ";
}

public class RegisterLoginManagement : MonoBehaviour
{
    private readonly LoginData _loginData = new LoginData();

    public List<string> ExistingUsernames;
    public string[] ExistingPasswords;
    public Image TogglePasswordCensorIcon;
    public Sprite Locked;
    public Sprite unlocked;
    public TMPro.TextMeshProUGUI UsernameHud;

    public TMPro.TMP_InputField usernameInput;
    public TMPro.TMP_InputField passwordInput;
#pragma warning disable CS0414
    private bool _validPassword;
#pragma warning restore CS0414
#pragma warning disable CS0414
    private bool _validUsername;
#pragma warning restore CS0414
#pragma warning disable CS0414
    private bool _loggedIn;
#pragma warning restore CS0414
    private string _urlGameServer;
    private string _pathUpdateCredentials;
#pragma warning disable CS0414
    private string _pathUpdateFeedback;
#pragma warning restore CS0414
    private string _pathCheckUserName;
    private string _pathCheckUsernameAndPassword;
    private string _jsonData;
    [FormerlySerializedAs("LoggedInUser")] public string loggedInUser;

    // Start is called before the first frame update
    void Start()
    {
        _loggedIn = false;
        _urlGameServer = "https://TQLOBBSN2N5PMVQY.anvil.app/IANHMSZIEXYQHRVG3CB6WIA4/_/api/";
        _pathUpdateCredentials = "credentials";
        _pathUpdateFeedback = "playtestdata";
        _pathCheckUserName = "checkusername";
        _pathCheckUsernameAndPassword = "checkusernameandpassword";

        passwordInput.contentType = TMPro.TMP_InputField.ContentType.Password;
        TogglePasswordCensorIcon.sprite = Locked;

        _validPassword = false;
        _validUsername = false;
    }

    public void clearFields()
    {
        usernameInput.text = "";
        passwordInput.text = "";
        _validPassword = false;
        _validUsername = false;
    }

    public void GoToMainMenu()
    {
        //Debug.Log("Turn me off");

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
           // Debug.Log("Valid username and password Length");
            _loginData.username = usernameInput.text;
            _loginData.password = passwordInput.text;
            clearFields();
            _jsonData = JsonUtility.ToJson(_loginData);
            StartCoroutine(LogInAccount(_jsonData));
        }
        else
        {
           // Debug.Log("Passwords and Usernames need to be at least 6 characters long");
        }
    }

    public IEnumerator LogInAccount(string jsonData)
    {
        //Debug.Log("Made It this far");

        string url = _urlGameServer + _pathCheckUsernameAndPassword;

        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (request.downloadHandler.text == "CanLogIn")
            {
                //Debug.Log("Can log in valid details entered");
                UsernameHud.text = _loginData.username;
                _loggedIn = true;
                GoToMainMenu();
            }
            else
            {
                //Debug.Log("Can't log in invalid details entered");
            }

            if (!request.isNetworkError && request.responseCode == (int)HttpStatusCode.OK)
            {
                //Debug.Log("Downloaded : " + request.downloadHandler.text);
                //Debug.Log("Data successfully sent to the server");

            }
            else
            {
                Debug.Log("Error sending data to the server: Error " + request.responseCode);
            }
        }
    }

    public void RegisterData()
    {
        //Debug.Log("getting Data");
        RemoveSpaces();
        if (passwordInput.text.Length >= 6 && usernameInput.text.Length >=6)
        {
            _loginData.username = usernameInput.text;
            _loginData.password = passwordInput.text;
            clearFields();
            _jsonData = JsonUtility.ToJson(_loginData);
            StartCoroutine(RegisterUsername(_jsonData));
        }
        else
        {
            //Debug.Log("Passwords and Usernames need to be at least 6 characters long");
        }
        // StartCoroutine(GetUsername());
    }

    public IEnumerator RegisterUsername(string jsonData)
    {
        string url = _urlGameServer + _pathCheckUserName;

        using (UnityWebRequest request = UnityWebRequest.Put(url, jsonData))
        {
            request.method = UnityWebRequest.kHttpVerbPOST;
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            yield return request.SendWebRequest();

            if (request.downloadHandler.text == "NotTaken")
            {
                StartCoroutine(PostMethod(jsonData, _pathUpdateCredentials));
            }
            else
            {
                _validUsername = false;
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