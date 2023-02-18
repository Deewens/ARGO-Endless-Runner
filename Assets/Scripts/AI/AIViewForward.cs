// Coders:
// Caroline Percy
// ...

using UnityEngine;

/// <summary>
/// Is a script that keeps track of what the runner AI "Sees".
/// </summary>
public class AIViewForward : MonoBehaviour
{
    /// <summary>
    /// Occurs when the collider that acts as the AI's eyes "sees" something. (aka collides with it)
    /// </summary>
    /// <param name="other">The object that it "sees" ahead.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Decoration"))
            return;
        
        transform.parent.GetComponent<AI_Brain>().React(other);
    }
}