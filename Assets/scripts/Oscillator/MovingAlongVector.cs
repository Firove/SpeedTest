using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAlongVector : MonoBehaviour
{
    private Vector2 _Dir = Vector2.up;
    // private float _Spd = 1.0f;
    public float _Radius = 1.75f;
    public float _ZBias = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Randomize();
    }

    private void Randomize()
    {
        _Dir = (Random.insideUnitCircle).normalized;
        //RandPosInRadius();
    }

    private void RandPosInRadius()
    {
        Vector2 lpos2 = (Vector2)(Random.insideUnitCircle).normalized;
        transform.localPosition = lpos2 * Random.Range(0.0f,_Radius);
    }

    public void Bounce()
    {
        Vector2 locPos = (Vector2)transform.localPosition;
        if(locPos.magnitude>_Radius)
        {
            locPos = locPos.magnitude * locPos.normalized * 0.99f;
            //Vector3 Reflect(Vector3 inDirection, Vector3 inNormal);返回反射向量Result
            _Dir = Vector3.Reflect(
                (Vector3)_Dir,
                (Vector3)transform.localPosition.normalized);
        }
    }

    private float _PrevF = float.NegativeInfinity;
    // Update is called once per frame
    void Update()
    {
        var grp = gameObject.GetComponentInParent<OscillatorGroup>();

        float f = grp.GetFreq();
        if(!Mathf.Approximately(f,_PrevF))
        {
            RandPosInRadius();
        }
        _PrevF = f;

        Vector2 movement = Time.deltaTime * _Dir * f;
        Vector3 pos = transform.position;
        pos += (Vector3)movement;
        transform.position = pos;

        Bounce();
    }
}
