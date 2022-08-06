using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTreeSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetChop()
    {
        PlayerController.goLeftNo = 0;
    }

    public void SetShift()
    {
        PlayerController.goLeftNo = 1;
    }
}
