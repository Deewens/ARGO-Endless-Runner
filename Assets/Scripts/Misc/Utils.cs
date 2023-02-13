//Author : Izabela Zelek, February 2023

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class which adds a Util function that changes positions from Screen to World
/// </summary>
public class Utils : MonoBehaviour
{
   public static Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
}
