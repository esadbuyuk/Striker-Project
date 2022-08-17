using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefenderTypeChooser : MonoBehaviour
{    
    private int defenderType;
    public bool slider { get; private set; }
    public bool timer { get; private set; }
    private DestroyOutOfBounds destroyOutOfBounds;


    void OnDisable()
    {
        if (destroyOutOfBounds.ResetDefender)
        {
            slider = false;
            timer = false;

            ReselectDefenderType();
        }
    }


    private void Awake()
    {
        destroyOutOfBounds = gameObject.GetComponent<DestroyOutOfBounds>();

    }

    // Start is called before the first frame update
    void Start()
    {
        defenderType = Random.Range(1, 4);  // 2 1 yap�lcak

        if (defenderType == 1)
        {
            slider = true;
        }
        else
        {
            timer = true;
            // stealTime = Random.Range(1, 3);
            // Debug.Log("steal time for " + this.gameObject.name);
        }
    }

    public void ReselectDefenderType()
    {        
        defenderType = Random.Range(1, 4);  // 2 1 yap�lcak

        if (defenderType == 1)
        {
            // return "slider"; // return ile kullanabilmen için her frame de fonksiyonu tekrar dan çağırman gerekiyor. Ama bu fonksiyonu sadece awake olduğunda çağırıyorsun.
            slider = true;
        }
        else
        {
            // return "timer";
            timer = true;
            // stealTime = Random.Range(1, 3);
           // Debug.Log("steal time for " + this.gameObject.name);
        }
    }
}
