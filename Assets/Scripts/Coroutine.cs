using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour
{
    IEnumerator Timer(Action function, int seconds)
    {        
        yield return new WaitForSeconds(seconds);        
        
        function?.Invoke();
    }

    public void StartTimer(Action function, int seconds)
    {
        StartCoroutine(Timer(function, seconds));
    }
}
