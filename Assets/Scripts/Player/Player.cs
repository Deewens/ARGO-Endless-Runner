using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [field: SerializeField] public PlayerType PlayerType { get; private set; }
    
}
