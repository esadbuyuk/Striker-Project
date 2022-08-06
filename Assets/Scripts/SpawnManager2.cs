using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager2 : MonoBehaviour
{    
    public bool StopSpawners { get; private set; }
    public int SpawnedDefenders { get; private set; }
    public int DestroyedDefenders { get; private set; }
    private Dictionary<Spawner, int> SpawnersDict = new Dictionary<Spawner, int>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnedDefenders = 0;
        DestroyedDefenders = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToSpawnersList(Spawner spawner, int freaquency)
    {
        SpawnersDict.Add(spawner, freaquency);
    }

    public bool StopAllSpawners()
    {
        StopSpawners = true;
        return StopSpawners;
    }

    public void ActivateSpawners()
    {
        StopSpawners = false;        

        foreach (KeyValuePair<Spawner, int> spawner in SpawnersDict)
        {
            if (spawner.Value == 1)
            {
                spawner.Key.CallSpawnTimer();
            }
        }        
    }   

    public void IncreaseSpawnedDefs()
    {
        SpawnedDefenders += 1;
        // Debug.Log("SpawnedDef = " + SpawnedDefenders);
    }

    public void IncreaseDestroyedDefs()
    {
        DestroyedDefenders += 1;
        SpawnWithFreaquency();
    }    

    private void SpawnWithFreaquency()
    {
        foreach (KeyValuePair<Spawner, int> spawner in SpawnersDict)
        {
            if (spawner.Value > 1 && DestroyedDefenders > 0) // to prevent divide by zero error
            {
                if (DestroyedDefenders % spawner.Value == 0) // repeat spawning according to number of spawned defenders.
                {
                    spawner.Key.CallSpawnTimer();
                }
            }
        }
    }
}
