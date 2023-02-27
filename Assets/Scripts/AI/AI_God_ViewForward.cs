using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_God_ViewForward : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        transform.parent.GetComponent<AIBrain>().reactToAI_God(other);
    }
}
