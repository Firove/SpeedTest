using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FA
{
    //public float _SpdMultiplierMin, _SpdMultiplierMax;
    [System.Serializable]
    public class EventWithVector3 : UnityEvent<Vector3> { };
    [System.Serializable]
    public class EventWithFloat : UnityEvent<float> { };
}

