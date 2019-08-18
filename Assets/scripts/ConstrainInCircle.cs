using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstrainInCircle : MonoBehaviour
{
    public float _Radius = 1.0f;
    public float _ZBias = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Randomize();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 LPos2 = (Vector2)transform.localPosition;
        float rsqr = LPos2.sqrMagnitude;
        if(rsqr>=(_Radius*_Radius))
        {
            Randomize();
        }
    }

    [ContextMenu("Randomize")]
    public void Randomize()
    {
        //float r = Random.Range()
        Vector3 pos = _Radius * Random.insideUnitCircle;
        pos.z = _ZBias;

        //_VibVec = Vector3.Reflect((Vector3)_VibVec, (Vector3)transform.localPosition.normalized);

        transform.localPosition = pos;
    }
}
