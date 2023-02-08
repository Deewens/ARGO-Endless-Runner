using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_ViewForward : MonoBehaviour
{
    /// <summary>
    /// Occurs when the collider that acts as the AI's eyes "sees" something. (aka collides with it)
    /// </summary>
    /// <param name="other">The object that it "sees" ahead.</param>
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<AI_Brain>().React(other);
    }
}
