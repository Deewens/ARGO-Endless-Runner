using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner _groundSpawner;

    // Start is called before the first frame update
    void Start()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _groundSpawner.SpawnTile();
            Destroy(gameObject, 2);
        }
    }
}
