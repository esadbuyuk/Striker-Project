using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutine : MonoBehaviour
{
    IEnumerator FunctionTimer(Action function, int seconds)
    {        
        yield return new WaitForSeconds(seconds);        
        
        function?.Invoke();
    }

    public void StartFunctionTimer(Action function, int seconds)
    {
        StartCoroutine(FunctionTimer(function, seconds));
    }
}
