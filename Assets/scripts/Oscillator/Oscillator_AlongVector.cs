using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Oscillator_AlongVector : MonoBehaviour
{
    private Vector2 _VibVec = Vector2.up;
    private float _Phase = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Randomize();
    }

    private void Randomize()
    {
        //_VibVec = Vector3.Reflect((Vector3)_VibVec, (Vector3)transform.localPosition.normalized);
        _VibVec = Random.insideUnitCircle * Random.Range(0.5f, 2.0f);
        _Phase = Random.Range(0, Mathf.PI * 2.0f);
        transform.localScale = Random.Range(0.1f, 0.3f) * Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        var grp =gameObject.GetComponentInParent<OscillatorGroup>();
        float v = Mathf.Sin(grp.GetFreq() * Time.realtimeSinceStartup + _Phase);
        Vector3 locPos = _VibVec * v;
        transform.localPosition = locPos;
    }
}
