using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingAlongNoiseDir : MonoBehaviour
{
    private Vector2 _Dir = Vector2.up;

    public float _NoiseSpd = 1.0f;
    private Vector2 _NoiseDir;
    private Vector2 _NoiseStart;

    // private float _Spd = 1.0f;
    public float _Radius = 3.5f;
    public float _ZBias = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Randomize();
    }

    private void Randomize()
    {
        _NoiseDir = Random.insideUnitCircle.normalized;
        _NoiseStart = Random.insideUnitCircle * 100.0f;
    }

    private void RandPosInRadius()
    {
        Vector2 lpos2 = ((Vector2)Random.insideUnitCircle).normalized;
        transform.localPosition = lpos2 * Random.Range(0.0f, _Radius);
    }

    public void Bounce()
    {
        Vector2 locPos = (Vector2)transform.localPosition;
        if (locPos.magnitude > _Radius)
        {
            locPos = locPos.magnitude * locPos.normalized * 0.99f;
            _Dir = Vector3.Reflect(
                (Vector3)_Dir,
                (Vector3)transform.localPosition.normalized);
        }
    }

    public void NoiseDir()
    {
        Vector2 NPos = _NoiseStart + _NoiseDir * _NoiseSpd * Time.realtimeSinceStartup;
        float angle = 720.0f* Mathf.PerlinNoise(NPos.x, NPos.y);
        _Dir = Quaternion.AngleAxis(angle,Vector3.forward) * Vector2.right;
    }

    private float _PrevF = float.NegativeInfinity;
    // Update is called once per frame
    void Update()
    {
        NoiseDir();

        var grp = gameObject.GetComponentInParent<OscillatorGroup>();

        float f = grp.GetFreq();
        if (!Mathf.Approximately(f, _PrevF))
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
