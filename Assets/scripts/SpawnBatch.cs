using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBatch : MonoBehaviour
{
    public GameObject _prefab;
    public int _batchCnt = 100;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        for(int i=0;i<_batchCnt;i++)
        {
            GameObject newObj = Instantiate(_prefab, transform);
        }
    }
}
