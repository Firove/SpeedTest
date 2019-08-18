using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator_Rolling : MonoBehaviour
{
    public float _AngSpdMultiplier = 1.0f;
    private float _Phase = 0.0f;
    public bool _CCDir = true;

    // Start is called before the first frame update
    void Start()
    {
        Randomize();
    }

    private void Randomize()
    {
        _Phase = Random.Range(0, 360.0f);
        transform.localScale = Random.Range(0.1f, 0.3f) * Vector3.one;
        if(Random.value>0.5f)
        {
            _CCDir = true;
        }
        else
        {
            _CCDir = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var grp = gameObject.GetComponentInParent<OscillatorGroup>();
        float Freq = grp.GetFreq();
        float angSpd = Freq * Time.realtimeSinceStartup + _Phase;
        if(_CCDir==false)
        {
            angSpd = -angSpd;
        }

        float theta = Time.realtimeSinceStartup * angSpd * _AngSpdMultiplier;

        Quaternion rot = Quaternion.AngleAxis(theta, Vector3.forward);
        transform.rotation = rot;
    }
}
