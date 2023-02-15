using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class used in Observer Pattern
/// Deals with reacting to notification types
/// </summary>
public abstract class Observer : MonoBehaviour
{
    public abstract void OnNotify(object value, int collValue, NotificationType notification);
}
/// <summary>
/// Abstract class for a Subject, used in Observer Pattern
/// Registers and Unregisters Observers and deals with sending notifications to Observers
/// </summary>
public abstract class Subject : MonoBehaviour
{
    private List<Observer> _observers = new List<Observer>();

    public void RegisterObserver(Observer observer)
    {
        _observers.Add(observer);
    }

    public void UnregisterObserver(Observer observer)
    {
        _observers.Remove(observer);
    }

    /// <summary>
    /// Loops through each observer in the list and notifies them of a new notification
    /// <summary>
    public void Notify(object value, int collValue, NotificationType notificationType)
    {
        foreach(var observer in _observers)
        {
            observer.OnNotify(value, collValue, notificationType);
        }
    }
}

public enum NotificationType
{
    Hit, Move, Stop, Jump, Slide, Die
}
