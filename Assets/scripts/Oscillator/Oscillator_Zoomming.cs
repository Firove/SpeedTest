using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Oscillator_Zoomming : MonoBehaviour
{
    public float _SclMin = 0.1f;
    public float _SclMax = 0.3f;
    private float _Phase = 0.0f;


    public float _RandSclMin = 0.05f;
    public float _RandSclMax = 0.8f;
    public UnityEvent _FreqChanged;

    // Start is called before the first frame update
    void Start()
    {
        Randomize();
        _FreqChanged.Invoke();
    }

    private void Randomize()
    {
        _SclMin = Random.Range(_RandSclMin, _RandSclMax);
        _SclMax = Random.Range(_RandSclMin, _RandSclMax);
        _Phase = Random.Range(0, 36000.0f);

        // transform.localScale = Random.Range(_SclMin,_SclMax) * Vector3.one;
    }

    private float _PrevFreq = 0.0f;
    // Update is called once per frame
    void Update()
    {
        var grp = gameObject.GetComponentInParent<OscillatorGroup>();
        float Freq = grp.GetFreq();
        if (System.Math.Abs(Freq - _PrevFreq) > Mathf.Epsilon)
        {
            _FreqChanged.Invoke();
        }
        _PrevFreq = Freq;

        float sinVal = Mathf.Sin(Time.realtimeSinceStartup * Freq + _Phase);
        float zoom = (_SclMax - _SclMin) * (sinVal + 1.0f) / 2.0f + _SclMin;
        Vector3 lScl = Vector3.one * zoom;

        transform.localScale = lScl;
    }
}
