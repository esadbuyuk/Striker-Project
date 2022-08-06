using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerDesign : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FastStrikerSelected()
    {
        PlayerController.sprintSpeed = 8;
        PlayerController.shContestSpeed = 3.2f;
        MoveForward.shootSpeed = 17;
    }

    public void StrongStrikerSelected()
    {
        PlayerController.sprintSpeed = 5.5f;
        PlayerController.shContestSpeed = 4.2f;
        MoveForward.shootSpeed = 27;
    }
}
