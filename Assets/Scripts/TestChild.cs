using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChild : TestAndRecognize
{

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(esadIsHandsome);

    }

    // Update is called once per frame
    void Update()
    {
        base.esadIsHandsome = true;

    }
}
