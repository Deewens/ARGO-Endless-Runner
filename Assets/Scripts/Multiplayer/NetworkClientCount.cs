using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkClientCount : NetworkBehaviour
{
    [SyncVar]
    public int playersConnected;
}
