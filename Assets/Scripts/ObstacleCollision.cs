using UnityEngine;

public class ObstacleCollision : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Runner")
        {
            Debug.Log("GameOver");
            Destroy(gameObject);
        }
    }
}
