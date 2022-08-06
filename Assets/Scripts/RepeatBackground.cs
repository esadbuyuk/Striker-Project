using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    public Vector3 repeatoffset;
    public Vector3 lateralrepeatoffset;
    private new Camera camera;
    private float bottomBound;
    private float topBound;
    private float rightBound;
    private float leftBound;
    private float pitchHeight = 10.25f;
    private float pitchWidth = 10.25f;
    


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        camera = Camera.main; //GameObject.Find("Main Camera");             
    }

    void Update()
    {
        topBound = camera.transform.position.y + camera.orthographicSize; // The camera's height is twice its orthographic size
        bottomBound = camera.transform.position.y - camera.orthographicSize;
        rightBound = camera.transform.position.x + camera.orthographicSize * camera.aspect; // width = height * cam.aspect;
        leftBound = camera.transform.position.x - camera.orthographicSize * camera.aspect;  
    }



    void LateUpdate() // update ya�lcak
    { 
        if (transform.position.y + (pitchHeight / 2) * 5 < topBound)
        {
            transform.position = transform.position + repeatoffset;
        }
        else if (transform.position.y - (pitchHeight / 2) * 5 > bottomBound)
        {
            transform.position = transform.position - repeatoffset;
        }
       
        if (transform.position.x + (pitchWidth / 2) * 5 < rightBound)
        {
            transform.position = transform.position + lateralrepeatoffset;
        }
        else if (transform.position.x - (pitchWidth / 2) * 5 > leftBound)
        {
            transform.position = transform.position - lateralrepeatoffset;           
        }


        /*
        if (transform.position.y + camera.orthographicSize < camera.transform.position.y )
        {
            transform.position = transform.position + repeatoffset;
        }

        if (transform.position.x + 16 < camera.transform.position.x)
        {
            transform.position = transform.position + lateralrepeatoffset;
        }

        if (transform.position.x - 16 > camera.transform.position.x)
        {
            transform.position = transform.position - lateralrepeatoffset;
        }*/
    }
}
