using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;


public class DestroyOutOfBounds : MonoBehaviour
{
    private new Camera camera;
    private float bottomBound;
    private GameManager gameManager;
    [SerializeField]
    private TextMeshProUGUI destroyedtext;
    public bool ResetDefender { get; private set; }
    private Spawner spawner;
    private GameObject spawnManager;
    private SpawnManager2 spawnManager2;
    

    void OnEnable()
    {
        ResetDefender = false;
    }


    private void Awake()
    {
        ResetDefender = false;

        camera = Camera.main;     
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        spawner = gameObject.GetComponent<Spawner>();
        spawnManager = GameObject.Find("SpawnManager");
        spawnManager2 = spawnManager.GetComponent<SpawnManager2>();
    }

    // Update is called once per frame
    void Update()
    {
        bottomBound = camera.transform.position.y - camera.orthographicSize;

        if (transform.position.y < bottomBound - 0.5f)
        {
            ResetDefender = true;
            if (ResetDefender)
            {
                gameObject.SetActive(false);
                spawnManager2.IncreaseDestroyedDefs();
                spawner.CallSpawnTimer();
                gameManager.DefenderDestroyed();
            }
        }
    }
}
