using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public new GameObject camera;
    private Vector3 instantiateOffsetR;
    private Vector3 instantiateOffsetL;
    private Vector3 instantiateOffsetGoal;        
    // private DestroyOutOfBounds destroyOutOfBounds;
    [SerializeField]
    private GameObject spawnManager;    
    private SpawnManager2 spawnManager2;
    private bool[] spawnPositions;
    public bool leftSpawnPos;
    public bool rightSpawnPos;
    public bool midSpawnPos;    
    [SerializeField] 
    private int spawnFrequency = 1;
    [SerializeField]
    private int waitingTimeForSpawn;
    [SerializeField]
    private bool spawnAlone;
    private GameObject timerManager;
    private Coroutine coroutine;

    

    // objects need be active for update and start work!
    private void Awake()
    {
        spawnPositions = new bool[] { leftSpawnPos, rightSpawnPos, midSpawnPos };

        spawnManager2 = spawnManager.GetComponent<SpawnManager2>();
        timerManager = GameObject.Find("TimerManager");
        coroutine = timerManager.GetComponent<Coroutine>();      

        spawnManager2.AddToSpawnersList(this, spawnFrequency);
    }

    void Start()
    {       
        // destroyOutOfBounds = gameObject.GetComponent<DestroyOutOfBounds>();
    }

    void Update()
    {        
        
    }

    public void CallSpawnTimer() // everytime when object get destroyed, spawn it.
    {
        if (!spawnManager2.StopSpawners)
        {
            if (spawnAlone)
            {
                spawnManager2.StopAllSpawners();
            }
            coroutine.StartTimer(Spawn, waitingTimeForSpawn);
        }
    }

    private void Spawn()
    {
        transform.position = SpawnPosCalculator();
        gameObject.SetActive(true);

        if (!spawnAlone)
        {
            spawnManager2.IncreaseSpawnedDefs();
        }
    }    

    private Vector3 SpawnPosCalculator()
    {
        if (leftSpawnPos)
        {
            instantiateOffsetL = new Vector3(-3, 9, 16);
            return camera.transform.position + instantiateOffsetL;
        }
        else if (rightSpawnPos)
        {
            instantiateOffsetR = new Vector3(3, 9, 16);
            return camera.transform.position + instantiateOffsetR;
        }
        else if (midSpawnPos)
        {
            instantiateOffsetGoal = new Vector3(0, 15, 11);
            return camera.transform.position + instantiateOffsetGoal;
        }
        else
        {
            Debug.LogWarning("spawn pozisyonu belirtilmedi!");

            instantiateOffsetGoal = new Vector3(0, 15, 11);
            return camera.transform.position + instantiateOffsetGoal;
        }        
    }
}
