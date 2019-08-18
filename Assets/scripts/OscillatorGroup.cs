using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OscillatorGroup : MonoBehaviour
{
    [Range(-1.0f, 4.0f)]
    public float _FreqExp = 1.0f;

    [System.Serializable]
    public class EventWithFloat : UnityEvent<float> { };
    //public EventWithFloat _Frequence;

    public float GetFreq()
    {
        float f = Mathf.Exp(_FreqExp);
        return f;
    }

    public void SetFreq(float F)
    {
        float fe = Mathf.Log(F, Mathf.Exp(1.0f));
        _FreqExp = fe;

        //InvokeFrequence();

        gameObject.BroadcastMessage("Convert", GetFreq()); 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InvokeFrequence()
    {
        //_Frequence.Invoke(GetFreq());
    }

    

}
