using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandPosInCircle : MonoBehaviour
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
        
    }

    [ContextMenu("Randomize")]
    public void Randomize()
    {
        //float r = Random.Range()
        Vector3 pos = _Radius * Random.insideUnitCircle;
        pos.z = _ZBias;
        
        transform.localPosition = pos;
    }

}
