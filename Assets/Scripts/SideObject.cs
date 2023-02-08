using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideObject : MonoBehaviour
{
    private SideObjectSpawner _sideObjectSpawner;

    // Start is called before the first frame update
    void Start()
    {
        _sideObjectSpawner = GameObject.FindObjectOfType<SideObjectSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Runner")
        {
            _sideObjectSpawner.SpawnTile();
            Destroy(gameObject, 2);
        }
    }
}
