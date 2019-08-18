using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broadcaster : MonoBehaviour
{
    public string MethodName = "";

    [ContextMenu("Broadcast")]
    public void Broadcast()
    {
        gameObject.BroadcastMessage(MethodName);
    }
}
