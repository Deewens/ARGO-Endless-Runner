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

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, -5) * Time.fixedDeltaTime;
    }
}
