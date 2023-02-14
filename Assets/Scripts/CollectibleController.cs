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
                GameObject.Find("PickupController").GetComponent<GoalController>().AddSpeedUp();
            }
            else if (gameObject.transform.tag == "SpeedDown")
            {
                GameObject.Find("PickupController").GetComponent<PickupController>().SpeedDown();
                GameObject.Find("PickupController").GetComponent<GoalController>().AddSpeedDown();
            }
            else if (gameObject.transform.tag == "MaxHealth")
            {
                if(GameObject.Find("RunnerPlayer").GetComponent<RunnerHealthController>().GetHealth() == GameObject.Find("RunnerPlayer").GetComponent<RunnerHealthController>().GetMaxHealth())
                {
                    GameObject.Find("PickupController").GetComponent<GoalController>().CheckHealth();
                }
                GameObject.Find("RunnerPlayer").GetComponent<RunnerHealthController>().InstaHeal();
            }
            else if (gameObject.transform.tag == "PartHealth")
            {
                GameObject.Find("RunnerPlayer").GetComponent<RunnerHealthController>().PartialHeal();
            }
            else if (gameObject.transform.tag == "Apple")
            {
                GameObject.Find("PickupController").GetComponent<GoalController>().AddApple();
            }
            else if (gameObject.transform.tag == "Pomegranate")
            {
                GameObject.Find("PickupController").GetComponent<GoalController>().AddPom();
            }
            Destroy(gameObject);
        }

    }
}
