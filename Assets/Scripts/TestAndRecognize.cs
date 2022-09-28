using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAndRecognize : MonoBehaviour
{
    public class User
    {
        public bool isAngry;
    }
    protected bool esadIsHandsome;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        User esad = new User();
        esad.isAngry = true;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(esadIsHandsome);
    }
}
