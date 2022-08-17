using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteFlipper
{
    private readonly Transform transformToFlip;

    public SpriteFlipper(Transform transformToFlip)
    {
        this.transformToFlip = transformToFlip;
    }
    

    public void FlipToRight()
    {
        transformToFlip.localScale = new Vector3(1f, 1f, 1f);
    }


    public void FlipToLeft()
    {
        transformToFlip.localScale = new Vector3(-1f, 1f, 1f);
    }
}