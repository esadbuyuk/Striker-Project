using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialSpawnManager : MonoBehaviour
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
    private int callTurn = 0;
    public TextMeshProUGUI tipText1;
    public TextMeshProUGUI tipText4;
    public TextMeshProUGUI tipText6;
    // private bool gameStopped = false; // I prefer to do this on GameManager script


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
        SpawnPosRCalculator();
        SpawnPosLCalculator();
        
        // goal = GameObject.Find("goal");
    }


    void LateUpdate()
    {

        if (callTurn == 0)
        {
            if (!routinIsOnL)
            {
                StartCoroutine(CallTimerLdef(Random.Range(1, 3)));                
            }

        }
        else if (callTurn == 1)
        {
            if (!lDef.activeInHierarchy && destroyOutOfBoundsL.resetLeft)
            {
                if (!routinIsOnR)
                {
                    StartCoroutine(CallTimerRdef(Random.Range(2, 4)));                    
                }

            }
        }
        else if (callTurn == 2)
        {
            if (!rDef.activeInHierarchy && destroyOutOfBoundsR.resetRight)
            {
                if (!routinIsOnGoal)
                {
                    StartCoroutine(CallTimerGoal(3));
                    callTurn += 1;

                    // goal aktifle�tir.
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
            defenderPrefabs[0].transform.position = spawnPosR;
            // defenderPrefabs[0].transform.rotation = defenderPrefabs[0].transform.rotation;
            defenderPrefabs[0].SetActive(true);
            GameObject clonedObject = defenderPrefabs[0];
            // GameObject clonedObject = (GameObject)Instantiate(defenderPrefabs[0], spawnPosR, defenderPrefabs[0].transform.rotation);
            clonedObject.name = defenderPrefabs[0].name; // prefablerin originaliyle ayn� adda  olmas� i�in
            defCalledNum += 1;
            callTurn += 1;
            StopGame();
            tipText4.gameObject.SetActive(true);
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
            callTurn += 1;
            StopGame();
            tipText1.gameObject.SetActive(true);
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
        StopGame();
        tipText6.gameObject.SetActive(true);
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
    }

    private void StopGame()
    {
        Time.timeScale = 0;
        // gameStopped = true; // I prefer to do this on GameManager script
    }*/
}
