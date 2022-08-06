using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    public GameObject egoist;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        // egoist = GameObject.Find("egoist");

        //ballOffset = new Vector3(0, 0.25, 2.25);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = egoist.transform.position + offset;
    }
}
