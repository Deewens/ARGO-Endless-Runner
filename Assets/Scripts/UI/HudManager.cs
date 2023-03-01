using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HudManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static HudManager Instance;

    private bool _loggedIn = false;
    private string _username = "";

    private void Awake()
    {
        // start of new code
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _username = GameObject.Find("PlayerUserName").GetComponent<TextMeshProUGUI>().text;
        Debug.Log(_username);

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool GetLoggedIn()
    {
        return _loggedIn;
    }

    public void SetLoggedIn()
    {

        _loggedIn = true;
    }

    public string GetUsername()
    {
        return _username;
    }
}
