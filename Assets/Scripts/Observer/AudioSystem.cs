using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Deals with calling the correct audio based on the passed Notification Type from Subject
/// </summary>
public class AudioSystem : Observer
{
    void Start()
    {
        /// <summary>
        /// Finds all objects of type PointOfInterest and registers them to the observer
        /// </summary>
        foreach (var poi in FindObjectsOfType<PointOfInterest>())
        {
            poi.RegisterObserver(this);
        }
    }

    /// <summary>
    /// Is called from a PointOfInterest script, takes in the name of the object, the layer that it collided with and the type of notification sent through
    /// </summary>
    public override void OnNotify(object value, int collValue, NotificationType notificationType)
    {
        /// <summary>
        /// Plays the 'Hit' audio when the player interacts with an obstacle
        /// </summary>
        if (notificationType == NotificationType.Hit && value.ToString() == "Player" && collValue == LayerMask.NameToLayer("Obstacle"))
        {
            transform.Find("Hit").gameObject.GetComponent<AudioSource>().Play();
        }
        /// <summary>
        /// Plays the 'Collect' audio when the player interacts with a collectible 
        /// </summary>
        else if (notificationType == NotificationType.Hit && value.ToString() == "Player" && collValue == LayerMask.NameToLayer("Item"))
        {
            transform.Find("Collect").gameObject.GetComponent<AudioSource>().Play();
        }
        /// <summary>
        /// Plays the 'CollectCoin' audio when the player interacts with a coin 
        /// </summary>
        else if (notificationType == NotificationType.Hit && value.ToString() == "Player" && collValue == LayerMask.NameToLayer("Coin"))
        {
            transform.Find("CollectCoin").gameObject.GetComponent<AudioSource>().Play();
        }
        /// <summary>
        /// Plays the 'Drink' audio when the player interacts with a potion 
        /// </summary>
        else if (notificationType == NotificationType.Hit && value.ToString() == "Player" && collValue == LayerMask.NameToLayer("Potion"))
        {
            transform.Find("Drink").gameObject.GetComponent<AudioSource>().Play();
        }
        /// <summary>
        /// Plays the 'Jump' audio when the player jumps 
        /// </summary>
        if (notificationType == NotificationType.Jump)
        {
            transform.Find("Jump").gameObject.GetComponent<AudioSource>().Play();
        }
        /// <summary>
        /// Plays the 'Slide' audio when the player Slide 
        /// </summary>
        if (notificationType == NotificationType.Slide)
        {
            transform.Find("Slide").gameObject.GetComponent<AudioSource>().Play();
        }

        AudioSource walk = transform.Find("Walk").gameObject.GetComponent<AudioSource>();

        if (notificationType == NotificationType.Move)
        {  
            if (!walk.isPlaying)
            {
                walk.Play();
            }
        }
        /// <summary>
        /// If any other notification type is sent through, the player walking sound stops
        /// </summary>
        else if(notificationType == NotificationType.Stop)
        {
            if (walk.isPlaying)
            {
                walk.Stop();
            }
        }
    }
}
