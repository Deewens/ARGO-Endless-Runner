// Coders:
// Caroline Percy
// ...

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is a script that keeps track of what the runner AI "Sees".
/// </summary>

public class AI_ViewForward : MonoBehaviour
{
    /// <summary>
    /// Occurs when the collider that acts as the AI's eyes "sees" something. (aka collides with it)
    /// </summary>
    /// <param name="other">The object that it "sees" ahead.</param>
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<HC_AI_Brain>().React(other);
    }
}
