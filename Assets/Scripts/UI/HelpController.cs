using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpController : MonoBehaviour
{
    private int _lastChild;

    void Start()
    {
        _lastChild = GameObject.Find("MenuManager").transform.childCount;   
    }

    public  void OpenHelp()
    {
        GameObject.Find("MenuManager").transform.GetChild(_lastChild - 1).gameObject.SetActive(true);
    }

    public void CloseHelp()
    {
        GameObject.Find("MenuManager").transform.GetChild(_lastChild - 1).gameObject.SetActive(false);
    }
}
