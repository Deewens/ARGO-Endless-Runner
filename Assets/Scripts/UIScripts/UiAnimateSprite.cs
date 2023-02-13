/// Author : Patrick Donnelly
/// Contributors : ---

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiAnimateSprite : MonoBehaviour
{
    private GameObject _uiRunner;
    private Image _image;

    public Sprite[] SpriteArray;
    public float Speed = .75f;

    private int IndexSprite;


    private void Start()
    {
        _uiRunner = GameObject.Find("UIRunner");
        _image = _uiRunner.GetComponent<Image>();
        StartCoroutine(PlayAnimation());
    }

    private void Update()
    {
        if (_uiRunner == null || _image == null)
        {
            Debug.Log("Null reference - cannot find UIRunner");
            _uiRunner = GameObject.Find("UIRunner");
            _image = _uiRunner.GetComponent<Image>();
        }
    }

    IEnumerator PlayAnimation()
    {
        yield return new WaitForSeconds(Speed);
        if (IndexSprite >= SpriteArray.Length)
        {
            IndexSprite = 0;
        }
        _image.sprite = SpriteArray[IndexSprite];
        IndexSprite += 1;
        StartCoroutine(PlayAnimation());
    }
}
