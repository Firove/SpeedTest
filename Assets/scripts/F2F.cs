using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class F2F : MonoBehaviour
{

    [System.Serializable]
    public class EventWithFloat : UnityEvent<float> { };
    public EventWithFloat _FloatOut;

    public float _InMultiplier = 1.0f;
    public float _OutMultiplier = 1.0f;
    public AnimationCurve _I2O;

    public void Convert(float inVal)
    {
        float OutVal = _OutMultiplier * _I2O.Evaluate(inVal * _InMultiplier);
        _FloatOut.Invoke(OutVal);
    }
}
