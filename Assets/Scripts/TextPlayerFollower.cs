using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextPlayerFollower : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public bool goalDef;

    void Update()
    {
        Vector3 wantedPos = Camera.main.WorldToScreenPoint(target.position + offset);
        transform.position = wantedPos;
            
        if (!target.gameObject.activeInHierarchy)           
        {          
            gameObject.SetActive(false);          
        }
        else if (goalDef)
        {
            gameObject.SetActive(true);
        }
        
    }
}
