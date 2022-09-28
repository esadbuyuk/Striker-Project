using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BallController : MonoBehaviour
{    
    private readonly float forwardSpeed = 4f;
    private float shootSpeed = 20f; // you checked the full kinematic contacts and continues detection in ball rb. // StrikerDesign ı duzelt
    private float spinSpeed = 1;
    private Vector3 shootDirection;
    private Animator ballAnim;
    private GameObject egoist;
    [SerializeField] private Vector3 ballOffset = new Vector3(0.100000001f, 0.100000001f, 2);
    private Quaternion rotationToUp;
    private GameManager gameManager;
    private GameObject goal;
    private Vector3 aim;
    private int gollednum = 0;
    private bool spinToRight = false;
    private bool spinToLeft = false;
    private bool alreadyDone = false;
    [Header("Tip Text Panel")]
    [SerializeField] private TextMeshProUGUI tipText7;
    [SerializeField] private TextMeshProUGUI tipClick7;
    [SerializeField] private TextMeshProUGUI tipText8;
    private int turn = 0;
    [SerializeField] private GameObject spawnManager;
    private SpawnManager2 spawnManager2;
    private GoalKeeperController goalKeeperController;
    private MovementController movementController;
    private bool shoot;
    private bool pass;
    private bool stop;

    private void Awake()
    {
        spawnManager2 = spawnManager.GetComponent<SpawnManager2>();
        ballAnim = GetComponent<Animator>();
        egoist = GameObject.Find("egoist");
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


    void LateUpdate()
    {
        if (stop)
        {

        }
        else if (shoot)
        {
            movementController.MoveWithDirection(shootDirection, shootSpeed);
            Roll();

            if (Input.GetMouseButtonDown(0) && Time.timeScale != 0) // give spin to ball
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

            if (spinToRight)
            {
                // transform.Translate(new Vector3(3f, 0f, 0f) * Time.deltaTime * spinSpeed);
                movementController.MoveWithDirection(new Vector3(3f, 0f, 0f), spinSpeed);
            }

            if (spinToLeft)
            {
                // transform.Translate(new Vector3(-3f, 0f, 0f) * Time.deltaTime * spinSpeed);
                movementController.MoveWithDirection(new Vector3(-3f, 0f, 0f), spinSpeed);
            }
        }
        else if (pass)
        {
            Roll();
            movementController.MoveWithDirection((aim - transform.position), forwardSpeed);
            // movementController.MoveToAim(aim, forwardSpeed);            
        }        
        else
        {
            Noneless();
            gollednum = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("egoist"))
        {
            stop = false;            
            pass = false;
        }

        if (col.gameObject.CompareTag("goal"))
        {
            stop = true;
            shoot = false;
            spinToRight = false;
            spinToLeft = false;
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
            shoot = false;
            pass = false;
            gameManager.GameOver();
            spinToRight = false;
            spinToLeft = false;
            ballAnim.SetBool("shoot_roll", false);
        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("defender_body"))
        {
            gameManager.Nutmeg();
        }
    }

    public void Shooted(Vector3 aim)
    {
        shootDirection = (aim - transform.position).normalized;
        shoot = true;

        goalKeeperController.JumpToBall(aim);

    }

    public void Passed(Vector3 aim)
    {
        this.aim = aim;
        pass = true;
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
