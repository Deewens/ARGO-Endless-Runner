using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _player;
    Vector3 _offset;

    // Start is called before the first frame update
    void Start()
    {
        _offset = transform.position - _player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = _player.position + _offset;

        transform.position = new Vector3(transform.position.x, transform.position.y, newPos.z);
    }
}
