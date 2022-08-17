using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{/*
    public GameObject[] defenderPrefabs;
    public new GameObject camera;
    private Vector3 instantiateOffsetR;
    private Vector3 spawnPosR;
    private Vector3 instantiateOffsetL;
    private Vector3 spawnPosL;
    private Vector3 instantiateOffsetGoal;
    public GameObject rDef;
    public GameObject lDef;
    public GameObject goal;
    private int defCalledNum = 0;
    private bool routinIsOnR;
    private bool routinIsOnL;
    private bool routinIsOnGoal;
    private bool goalIsCalled = false;
    private DestroyOutOfBounds destroyOutOfBoundsL;
    private DestroyOutOfBounds destroyOutOfBoundsR;


    void Awake()
    {        
        destroyOutOfBoundsR = rDef.GetComponent<DestroyOutOfBounds>();
        destroyOutOfBoundsL = lDef.GetComponent<DestroyOutOfBounds>();
    }


    void Start()
    {
        // goal = GameObject.Find("goal");
        // camera = GameObject.Find("Main Camera");

        // rDef = GameObject.Find("defender right");
        // lDef = GameObject.Find("defender left");
        // rDef.SetActive(false);  activeInHirearchy fonksiyonunu kullanabilmem i�in gerekli
        // lDef.SetActive(false); activeInHirearchy fonksiyonunu kullanabilmem i�in gerekli       
    }


    void Update()
    {
        SpawnPosRCalculator(); // bunları her frame de hesaplmana gerek yok!
        SpawnPosLCalculator();
       
        // goal = GameObject.Find("goal");
    }


    void LateUpdate()
    {
        
        if (!goalIsCalled)
        {
            if (!rDef.activeInHierarchy && destroyOutOfBoundsR.ResetDefender && defCalledNum < 4)
            {
                if (!routinIsOnR)
                {                    
                    StartCoroutine(CallTimerRdef(Random.Range(2, 4)));
                }

            }

            if ((!lDef.activeInHierarchy) && (destroyOutOfBoundsL.ResetDefender) && defCalledNum < 4)
            {
                if (!routinIsOnL)
                {
                    StartCoroutine(CallTimerLdef(Random.Range(1, 3)));
                }

            }
            
        }
        else
        {
            if (!goal.activeInHierarchy)
            {
                goalIsCalled = false;
            }
        }
       

        if (defCalledNum >= 4)
        {
            if (!routinIsOnGoal)
            {
                defCalledNum = 0;                
                StartCoroutine(CallTimerGoal(5));

                if ((!lDef.activeInHierarchy) && (destroyOutOfBoundsL.resetLeft))
                {
                    if (!routinIsOnL)
                    {
                        StartCoroutine(CallTimerLdef(Random.Range(3, 4)));
                        defCalledNum -= 1;
                    }

                }
                
                // goal aktifle�tir.
            }

        }       

        if (Input.GetKeyDown(KeyCode.P))
        {
            int defenderIndex = Random.Range(0, defenderPrefabs.Length);            

            if (!defenderPrefabs[defenderIndex].activeInHierarchy)
            {
                if (defenderIndex == 0) // sa�dan spawn edilicekler.
                {
                    defenderPrefabs[defenderIndex].transform.position = spawnPosR;
                    defenderPrefabs[defenderIndex].transform.rotation = defenderPrefabs[defenderIndex].transform.rotation;
                    defenderPrefabs[defenderIndex].SetActive(true);
                    GameObject clonedObject = defenderPrefabs[defenderIndex]; 
                    // GameObject clonedObject = (GameObject)Instantiate(defenderPrefabs[defenderIndex], spawnPosR, defenderPrefabs[defenderIndex].transform.rotation);               
                    clonedObject.name = defenderPrefabs[defenderIndex].name; // prefablerin original olmas� i�in
                }              
                else if (defenderIndex == 1) // soldan spawn edilicekler.
                {
                    defenderPrefabs[defenderIndex].transform.position = spawnPosL;
                    defenderPrefabs[defenderIndex].transform.rotation = defenderPrefabs[defenderIndex].transform.rotation;
                    defenderPrefabs[defenderIndex].SetActive(true);
                    GameObject clonedObject = defenderPrefabs[defenderIndex];
                    // GameObject clonedObject = (GameObject)Instantiate(defenderPrefabs[defenderIndex], spawnPosL, defenderPrefabs[defenderIndex].transform.rotation);
                    clonedObject.name = defenderPrefabs[defenderIndex].name;
                }

            }               

        }

    }    

    IEnumerator CallTimerRdef(int seconds)
    {
        routinIsOnR = true;
        yield return new WaitForSeconds(seconds);
        routinIsOnR = false;

        if (!goalIsCalled)
        {
            rDef.transform.position = spawnPosR;
            // defenderPrefabs[0].transform.rotation = defenderPrefabs[0].transform.rotation;
            rDef.SetActive(true);
            //GameObject clonedObject = defenderPrefabs[0];
            //clonedObject.name = defenderPrefabs[0].name; // prefablerin originaliyle ayn� adda  olmas� i�in
            defCalledNum += 1;
        }
    }

    IEnumerator CallTimerLdef(int seconds)
    {
        routinIsOnL = true;
        yield return new WaitForSeconds(seconds);
        routinIsOnL = false;

        if (!goalIsCalled)
        {
            defenderPrefabs[1].transform.position = spawnPosL;
            // defenderPrefabs[1].transform.rotation = defenderPrefabs[1].transform.rotation;
            defenderPrefabs[1].SetActive(true);
            GameObject clonedObject = defenderPrefabs[1];
            // GameObject clonedObject = (GameObject)Instantiate(defenderPrefabs[1], spawnPosL, defenderPrefabs[1].transform.rotation);
            clonedObject.name = defenderPrefabs[1].name; // prefablerin originaliyle ayn� adda olmas� i�in            
            defCalledNum += 1;
        }
    }

    IEnumerator CallTimerGoal(int seconds)
    {
        routinIsOnGoal = true;
        yield return new WaitForSeconds(seconds);
        routinIsOnGoal = false;

        
        instantiateOffsetGoal = new Vector3(0, 15, 11);
        goal.transform.position = camera.transform.position + instantiateOffsetGoal;
        goal.SetActive(true);
        goalIsCalled = true;
    }

    private Vector3 SpawnPosRCalculator()
    {
        instantiateOffsetR = new Vector3(3, 9, 16);
        spawnPosR = camera.transform.position + instantiateOffsetR;

        return spawnPosR;
    }


    private Vector3 SpawnPosLCalculator()
    {
        instantiateOffsetL = new Vector3(-3, 9, 16);
        spawnPosL = camera.transform.position + instantiateOffsetL;

        return spawnPosL;
    }*/
}
