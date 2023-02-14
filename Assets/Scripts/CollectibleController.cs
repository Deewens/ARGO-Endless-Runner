using System;
using UnityEngine;

/// <summary>
/// Handles the destruction and score of pickups
/// </summary>
public class CollectibleController : MonoBehaviour
{
    public int Score;
    public static event Action OnPlayerDeath;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Inpenetrable" || other.tag == "JumpObstacle" || other.tag == "SlideObstacle")
        {
            Destroy(gameObject);
        }
        else if (other.tag == "BehindPlayer")
        { 
            Destroy(gameObject);
        }
        else if (other.tag == "Runner")
        {
            if(gameObject.transform.tag == "SpeedUp")
            {
                GameObject.Find("PickupController").GetComponent<PickupController>().SpeedUp();
            }
            else if (gameObject.transform.tag == "SpeedDown")
            {
                GameObject.Find("PickupController").GetComponent<PickupController>().SpeedDown();
            }
            else if (gameObject.transform.tag == "MaxHealth")
            {
                GameObject.Find("RunnerPlayer").GetComponent<RunnerHealthController>().InstaHeal();
            }
            else if (gameObject.transform.tag == "PartHealth")
            {
                GameObject.Find("RunnerPlayer").GetComponent<RunnerHealthController>().PartialHeal();
            }
            Destroy(gameObject);
        }

    }
}
