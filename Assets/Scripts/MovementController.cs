using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController
{
    private readonly Transform transformToMove;
    private readonly AttributeSettings attributeSettings;


    public MovementController(Transform transformToMove, AttributeSettings attributeSettings)
    {
        this.transformToMove = transformToMove;
        this.attributeSettings = attributeSettings;
    }


    public void MoveWithWheel()
    {
        transformToMove.Rotate(attributeSettings.TurnSpeed * Time.deltaTime * Vector3.up);
        transformToMove.position += attributeSettings.SprintSpeed * Time.deltaTime * transformToMove.forward;
    }   

    public void MoveToAim(Vector3 aimPosition, float? moveSpeed = null) 
    {
        if (moveSpeed == null)
        {
            Vector3 lookdirection = aimPosition - transformToMove.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookdirection);

            transformToMove.rotation = rotation;
            transformToMove.Translate(attributeSettings.SprintSpeed * Time.deltaTime * Vector3.up);
        }
        else
        {
            Vector3 lookdirection = aimPosition - transformToMove.position;
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookdirection);

            transformToMove.rotation = rotation;
            transformToMove.Translate((Vector3)(moveSpeed * Time.deltaTime * Vector3.up));
        }            
    }

    public void MoveWithDirection(Vector3 direction, float? moveSpeed = null) // kontrol edilmedi.
    {
        if (moveSpeed == null)
        {
            transformToMove.Translate(attributeSettings.SprintSpeed * Time.deltaTime * direction); // direction ile niye çarptığımız hakkında emin değilim.
        }
        else
        {
            transformToMove.Translate((Vector3)(moveSpeed * Time.deltaTime * direction));
        }
    }
}

