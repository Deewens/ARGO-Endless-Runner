using UnityEngine;

public class GroundTile : MonoBehaviour
{
    private GroundSpawner _groundSpawner;

    // Start is called before the first frame update
    void Start()
    {
        _groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += new Vector3(0, 0, -10) * Time.fixedDeltaTime;
    }
}
