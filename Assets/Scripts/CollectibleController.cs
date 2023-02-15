/// Author :
/// Contributor : Patrick Donnelly - Score/runner Related elements

using System;
using UnityEngine;

/// <summary>
/// Handles the destruction and score of pickups
/// </summary>
public class CollectibleController : MonoBehaviour
{
    private int _pointsForCoins;
    public static event Action OnPlayerDeath;
    private GameObject _runner;
    private Score _runnerScoreScript;

    // Start is called before the first frame update
    void Start()
    {
        _pointsForCoins = 5;
        _runner = GameObject.Find("RunnerPlayer");
        _runnerScoreScript = _runner.GetComponent<Score>();
    }

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
                GetComponent<PointOfInterest>().StartHit(other);
            }
            else if (gameObject.transform.tag == "SpeedDown")
            {
                GameObject.Find("PickupController").GetComponent<PickupController>().SpeedDown();
                GameObject.Find("PickupController").GetComponent<GoalController>().AddSpeedDown();
                GetComponent<PointOfInterest>().StartHit(other);
            }
            else if (gameObject.transform.tag == "MaxHealth")
            {
                if(GameObject.Find("RunnerPlayer").GetComponent<RunnerHealthController>().GetHealth() == GameObject.Find("RunnerPlayer").GetComponent<RunnerHealthController>().GetMaxHealth())
                {
                    GameObject.Find("PickupController").GetComponent<GoalController>().CheckHealth();
                }
                GameObject.Find("RunnerPlayer").GetComponent<RunnerHealthController>().InstaHeal();
                GetComponent<PointOfInterest>().StartHit(other);
            }
            else if (gameObject.transform.tag == "PartHealth")
            {
                GameObject.Find("RunnerPlayer").GetComponent<RunnerHealthController>().PartialHeal();
                GetComponent<PointOfInterest>().StartHit(other);
            }
            else if (gameObject.transform.tag == "Apple")
            {
                GameObject.Find("PickupController").GetComponent<GoalController>().AddApple();
                GetComponent<PointOfInterest>().StartHit(other);
            }
            else if (gameObject.transform.tag == "Pomegranate")
            {
                GameObject.Find("PickupController").GetComponent<GoalController>().AddPom();
                GetComponent<PointOfInterest>().StartHit(other);
            }
            else if (gameObject.transform.tag == "Coin")
            {
                GetComponent<PointOfInterest>().StartHit(other);
                _runnerScoreScript.AddComboPoints(_pointsForCoins);
            }
            Destroy(gameObject);
        }

    }
}
