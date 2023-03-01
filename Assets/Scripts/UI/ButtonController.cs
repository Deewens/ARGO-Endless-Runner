using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private int _lastChild;

    void Start()
    {
        _lastChild = GameObject.Find("MenuManager").transform.childCount;   
    }

    public  void OpenHelp()
    {
        GameObject.Find("MenuManager").transform.GetChild(_lastChild - 2).gameObject.SetActive(true);
    }

    public void CloseHelp()
    {
        GameObject.Find("MenuManager").transform.GetChild(_lastChild - 2).gameObject.SetActive(false);
    }

    public void OpenSettings()
    {
        GameObject.Find("MenuManager").transform.GetChild(_lastChild - 1).gameObject.SetActive(true);
    }

    public void CloseSettings()
    {
        GameObject.Find("MenuManager").transform.GetChild(_lastChild - 1).gameObject.SetActive(false);
    }
}
