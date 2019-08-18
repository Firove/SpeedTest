using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandOscillatorValues : MonoBehaviour
{
    public OscillatorGroup _GrpA, _GrpB;

    public float _FreqExpA, _FreqExpB;


    public float _RandMin = 1.0f, _RandMax = 20.085f;

    [ContextMenu("Randomize")]
    public void Randomize()
    {
        float Freq1 = Random.Range(_RandMin, _RandMax);
        float Freq2 = Random.value * Freq1;

        if(Random.value<0.5f)
        {
            _GrpA.SetFreq(Freq1);
            _GrpB.SetFreq(Freq2);
        }
        else
        {
            _GrpA.SetFreq(Freq2);
            _GrpB.SetFreq(Freq1);
        }
        
    }
}
