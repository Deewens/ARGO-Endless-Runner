using UnityEngine;
/// <summary>
/// Upon colliding or movement change, sends notification to observer with Notification type
/// </summary>
public class PointOfInterest : Subject
{
    [SerializeField]
    private string poiName;
    private void Start()
    {
        poiName = gameObject.name;
        if (poiName != "Player")
        { 
            if (GameObject.FindObjectsOfType<AudioSystem>().Length > 0)
            {
                RegisterObserver(GameObject.Find("AudioManager").GetComponent<AudioSystem>());
            }

       }
    }

    /// <summary>
    /// Sends a Move notification when the gameobject when not Sliding or Jumping
    /// </summary>
    private void Update()
    {
        //if (this.gameObject == null)
        //{
        //    Notify(poiName, 0, NotificationType.Die);
        //}
        if (poiName == "Player")
        {
            if (!GetComponentInParent<RunnerPlayer>().sliding && !GetComponentInParent<RunnerPlayer>().jumping)
            {
                Notify(poiName, 0, NotificationType.Move);

            }
        }
        
    }
    ///// <summary>
    ///// Sends a Hit notification when the gameobject interacts with a trigger object
    ///// </summary>
    //private void OnTriggerEnter(Collider _otherColldier)
    //{
    //    if(poiName != "Player" && _otherColldier.transform.name != "Runner")
    //    {
    //    }
    //    else if (_otherColldier.transform.tag != "Untagged" && _otherColldier.transform.tag != "Ground")
    //    { 
    //        Notify(_otherColldier.gameObject.name, _otherColldier.gameObject.layer, NotificationType.Hit); 
    //    }
    //}
    ///// <summary>
    ///// Sends a Hit notification when the gameobject interacts with a collider object
    ///// </summary>
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag != "Untagged" && collision.transform.tag != "Ground")
    //    {
    //        Notify(collision.gameObject.name, collision.gameObject.layer, NotificationType.Hit);
    //    }
    //}

    /// <summary>
    ///Sends slide notification when the gameobject slides, sends stop notification
    /// </summary>
    public void StartSlide()
    {
        Notify(poiName, 0, NotificationType.Stop);
        Notify(poiName, 0, NotificationType.Slide);
    }
    /// <summary>
    ///Sends jump notification when the gameobject jumps, sends stop notification
    /// </summary>
    public void StartJump()
    {
        Notify(poiName, 0, NotificationType.Stop);
        Notify(poiName, 0, NotificationType.Jump);
    }

    public void StartHit(Collider collision)
    {
        Notify(collision.gameObject.name, gameObject.layer, NotificationType.Hit);

    }
}
