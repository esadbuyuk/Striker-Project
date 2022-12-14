using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class MoveForward : MonoBehaviour
{
    /* 
    private readonly float forwardSpeed = 15f;
    private float shootSpeed = 20f; // you checked the full kinematic contacts and continues detection in ball rb. // StrikerDesign ı duzelt
    private Vector3 shootDirection;
    private Animator ballAnim;
    private GameObject egoist;
    [SerializeField] private Vector3 ballOffset;
    private Camera cameraMain;
    private Quaternion rotationToUp;
    private Vector3 mousePos;
    private GameManager gameManager;
    private GameObject goal;
    public Vector3 Aim { get; private set; }
    private bool stopBall = false;
    private int gollednum = 0;
    private bool spinToRight = false;
    private bool spinToLeft = false;
    // [SerializeField] private TextMeshProUGUI highscoreText;
    private bool alreadyDone = false;
    [SerializeField] private TextMeshProUGUI tipText7;
    [SerializeField] private TextMeshProUGUI tipClick7;
    [SerializeField] private TextMeshProUGUI tipText8;
    private int turn = 0;
    [SerializeField] private GameObject spawnManager;
    private SpawnManager2 spawnManager2;
    private GoalKeeperController goalKeeperController;
    // [SerializeField] private AttributeSettings attributeSettings;
    private MovementController movementController;
    private bool move;
    private PlayerController playerController;

    private void Awake()
    {
        cameraMain = Camera.main;
        spawnManager2 = spawnManager.GetComponent<SpawnManager2>();
        ballAnim = GetComponent<Animator>();
        egoist = GameObject.Find("egoist");
        playerController = egoist.GetComponent<PlayerController>();
        goal = GameObject.Find("goal");
        goalKeeperController = GameObject.Find("goalkeeper").GetComponent<GoalKeeperController>();
        movementController = new MovementController(transform, null);
        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
        {
            shootSpeed = 20f;
        }        
        goal.SetActive(false); // activeInHirearchy fonksiyonunu kullanabilmem icin gerekli        
        rotationToUp = transform.rotation;
    }


    void Update() // burda lateUpdate kullanabilicegin bir yol dusun.
    {
        if (Input.GetMouseButtonDown(0) && playerController.HaveBall && !IsPointerOverUIObject() && Time.timeScale == 1) // bunu kapsülden çekiceksin: haveBall
        {
            move = true;
            AimCalculator();
        }

        if (move & stopBall)
        {
            MoveToAim();

            if (spinToRight)
            {
                transform.Translate(new Vector3(3f, 0f, 0f) * Time.deltaTime);
            }

            if (spinToLeft)
            {
                transform.Translate(new Vector3(-3f, 0f, 0f) * Time.deltaTime);
            }
        }
        else
        {
            Noneless();
            stopBall = false;
            gollednum = 0;            
        }
    } 


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("egoist"))
        {
            move = false;
            Debug.Log("Burası çalıştı.");
        }

        if (col.gameObject.CompareTag("goal"))
        {
            spinToRight = false;
            spinToLeft = false;
            PlayerController.shoot = false;
            ballAnim.SetBool("shoot_roll", false);
            if (gollednum > 0)
            {
                goal.SetActive(false);
                spawnManager2.ActivateSpawners();
            }
            if (gollednum == 0)
            {
                gameManager.GameOver();
            }
            stopBall = true;

            if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
            {
                StopGame();
                tipText8.gameObject.SetActive(true);
            }
        }

        if (col.gameObject.CompareTag("goal line"))
        {
            if (gollednum == 0)
            {
                gollednum += 1;
                gameManager.Goal();                
            }           
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("defender_feet_collider") || col.gameObject.CompareTag("goalkeeper"))
        {
            spinToRight = false;
            spinToLeft = false;
            PlayerController.shoot = false;
            ballAnim.SetBool("shoot_roll", false);
            stopBall = true;
            gameManager.GameOver();
        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("defender_body")) 
        {
            gameManager.Nutmeg();            
        }
    }


    private void MoveToAim()
    {
        if (IsAimOnGoal())
        {
            playerController.Shoot();
            movementController.MoveWithDirection(shootDirection, shootSpeed);
            Roll();
            goalKeeperController.JumpToBall();

            if (Input.GetMouseButtonDown(0) && Time.timeScale == 1) // give spin to ball
            {
                if (Input.mousePosition.x > Screen.width * 0.5f)
                {
                    spinToRight = true;

                    if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
                    {
                        tipClick7.gameObject.SetActive(false);
                    }
                }
                else
                {
                    spinToLeft = true;

                    if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
                    {
                        tipClick7.gameObject.SetActive(false);
                    }
                }
            }
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, Aim, forwardSpeed * Time.deltaTime); // burda movementController kullanmayı dene, aşağıda da aynısı var.
            Roll();
        }       
    }

    private bool IsAimOnGoal()
    {
        if (goal.activeInHierarchy)
        {
            if (goal.transform.position.y - 6 > Aim.y)
            {
                return true;
            }
            else
            {                
                return false;
            }

        }
        else
        {
            return false;
        }
    }


    private void AimCalculator()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 1;
        Aim = mousePos;
        shootDirection = (Aim - transform.position).normalized;
    }
    
    
    private void Noneless()
    {
        ballAnim.SetBool("roll", false);
        transform.SetPositionAndRotation(egoist.transform.position + ballOffset, rotationToUp);
    }


    private void Roll()
    {
        ballAnim.SetBool("roll", true);
    }

    private bool IsPointerOverUIObject()
    {
        // Referencing this code for GraphicRaycaster https://gist.github.com/stramit/ead7ca1f432f3c0f181f
        // the ray cast appears to require only eventData.position.
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void MiddleOfShooting()
    {
        if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
        {
            if (!alreadyDone)
            {
                StopGame();
                tipText7.gameObject.SetActive(true);
                tipClick7.gameObject.SetActive(true);
                alreadyDone = true;
                turn += 1;
            }  
            
            if (turn == 1)
            {
                StopGame();
                tipClick7.gameObject.SetActive(true);
                turn += 1;
            }
        }
    }


    private void StopGame()
    {
        Time.timeScale = 0;
        // gameStopped = true;
    }


    /*    
    void DecreaseForwardSpeed()
    {
        if (forwardSpeed > 0.0f)
        {
            forwardSpeed -= 2.5f;
        }
        Debug.Log(forwardSpeed);
    }

    void DecreaseHorizontalSpeed()
    {
        if (horizontalSpeed > 0.0f)
        {
            horizontalSpeed -= 0.5f;
        }
        Debug.Log(horizontalSpeed);
    }
     */
}


