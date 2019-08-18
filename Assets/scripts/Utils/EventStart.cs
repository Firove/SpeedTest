using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventStart : MonoBehaviour
{
    public UnityEvent _Start;
    // Start is called before the first frame update
    void Start()
    {
        _Start.Invoke();
    }
}
