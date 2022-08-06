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
    private float forwardSpeed = 15f;
    public static float shootSpeed = 20f; // you checked teh full kinematic contacts and continues detection in ball rb.
    private Vector3 shootDirection;
    // private float horizontalSpeed = 1.5f;
    // private bool haveBall = true;
    private Animator ballAnim;
    private GameObject egoist;
    // public GameObject goal;
    public Vector3 ballOffset;
    private Vector3 lookDirection;
    // public Vector3 aimOffset;
    // private Vector3 startRollPos;
    // private Rigidbody2D ballRb;
    // private new GameObject camera;        
    private TouchControls touchControls;
    private Camera cameraMain;
    private Quaternion rotationToUp;
    private Vector3 mousePos;
    private GameManager gameManager;
    public TextMeshProUGUI nutmegtext;
    public TextMeshProUGUI goaltext;
    private GameObject goal;
    public Vector3 aim;
    // private bool isWaitComplete = false;
    // public static bool shoot = false;
    private bool stopBall = false;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    private int gollednum = 0;
    private Vector3 firstPos;
    public bool spinToRight = false;
    public bool spinToLeft = false;
    public TextMeshProUGUI highscoreText;
    private bool alreadyDone = false;
    public TextMeshProUGUI tipText7;
    public TextMeshProUGUI tipClick7;
    public TextMeshProUGUI tipText8;
    private int turn = 0;
    [SerializeField]
    private GameObject spawnManager;
    private SpawnManager2 spawnManager2;

    private void Awake()
    {
        touchControls = new TouchControls();
        cameraMain = Camera.main;
        spawnManager2 = spawnManager.GetComponent<SpawnManager2>();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
        {
            shootSpeed = 20f;
        }

        ballAnim = GetComponent<Animator>();

        egoist  = GameObject.Find("egoist");

        goal = GameObject.Find("goal");
        goal.SetActive(false); // activeInHirearchy fonksiyonunu kullanabilmem i�in gerekli
        

        rotationToUp = transform.rotation;

        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();

        //ballOffset = new Vector3(0, 0.25, 2.25);
        //

        // ballRb = GetComponent<Rigidbody2D>();

        // camera = GameObject.Find("Main Camera");

        touchControls.Touch1.TouchPress.started += ctx => StartTouch(ctx);
        touchControls.Touch1.TouchPress.canceled += ctx => EndTouch(ctx);
    }



    void Update() // burda lateUpdate kullanabilice�in bir yol d���n.
    {      
       

        if (spinToRight)
        {
            transform.Translate(new Vector3(3f, 0f, 0f) * Time.deltaTime);
        }

        if (spinToLeft)
        {
            transform.Translate(new Vector3(-3f, 0f, 0f) * Time.deltaTime);

        }

        if (PlayerController.shoot)
        {
            if (Input.GetMouseButtonDown(0) && Time.timeScale == 1)
            {
                // mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

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

    }


    private void FixedUpdate()
    {
        if (PlayerController.haveBall)
        {
            Noneless();
            stopBall = false;
            gollednum = 0;
            // shoot = false;
            spinToRight = false;
            spinToLeft = false;
        }
        else if (!stopBall)
        {

            MoveToAim();

            /* ballRb.AddRelativeForce(lookDirection * forwardSpeed, (ForceMode2D)ForceMode.Impulse);

             transform.Translate(Vector3.up * Time.deltaTime * forwardSpeed);
             transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);  */

        }
    }

    public void SendBall()
    {
        // isWaitComplete = false;
        // haveBall = false;
        AimCalculator();
        Vector3 firstPos = transform.position;
        shootDirection = (aim - transform.position).normalized;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("egoist"))
        {           
            // isWaitComplete = false; haveBall = true
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
                GameOverf();
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
                gameManager.UpdateScore(500);
                goaltext.gameObject.SetActive(true);
                StartCoroutine(RemoveAfterSeconds(1.5f, goaltext.gameObject));
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
            GameOverf();
            Debug.Log("Game Over!");            
        }
    }


    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("defender_body"))
        {
            Debug.Log("nutmeg");
            gameManager.UpdateScore(50);
            nutmegtext.gameObject.SetActive(true);
            StartCoroutine(RemoveAfterSeconds(1, nutmegtext.gameObject)); // 0.0f de�i�tirilebilir!
        }

    }


    private void MoveToAim()
    {
        /*lookDirection = (aim - transform.position);
        Quaternion rotationToAim = Quaternion.LookRotation(Vector3.forward, lookDirection);

        transform.rotation = rotationToAim;
        transform.Translate(forwardSpeed * Time.deltaTime * Vector3.up);
        */
        if (goal.activeInHierarchy)
        {            
            if (!PlayerController.shoot)
            {                
                transform.position = Vector2.MoveTowards(transform.position, aim, forwardSpeed * Time.deltaTime);
                Roll();                
            }
            else
            {               
                transform.Translate(shootSpeed * Time.deltaTime * shootDirection);
                Roll();
                ballAnim.SetBool("shoot_roll", true);
                
            }

        }
        else 
        {            
            transform.position = Vector2.MoveTowards(transform.position, aim, forwardSpeed * Time.deltaTime);            
            Roll();

        }       

    }


   
    private void AimCalculator()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 1;
        aim = mousePos;
       
    }


    IEnumerator RemoveAfterSeconds(float seconds, GameObject obj)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }

    /*
    IEnumerator WaitShootAnim(float seconds)
    {
        if (!isWaitComplete)
        {
            yield return new WaitForSeconds(seconds);
            isWaitComplete = true;
        }
    }*/


    private void GameOverf()
    {
        Time.timeScale = 0;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        highscoreText.gameObject.SetActive(true);
        // isGameActive = false;
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


    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch started " + touchControls.Touch1.TouchPosition.ReadValue<Vector2>());
        //  if (OnStartTouch != null) OnStartTouch(touchControls.Touch1.TouchPosition.ReadValue<Vector2>(), (float)context.startTime);
        Vector3 screenCoordinates = new Vector3(touchControls.Touch1.TouchPosition.ReadValue<Vector2>().x, touchControls.Touch1.TouchPosition.ReadValue<Vector2>().y, cameraMain.nearClipPlane);
        Vector3 worldCoordinates = cameraMain.ScreenToWorldPoint(screenCoordinates);
        worldCoordinates.z = 0;
    }


    private void EndTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch ended" + touchControls.Touch1.TouchPosition.ReadValue<Vector2>());
        //  if (OnEndTouch != null) OnEndTouch(touchControls.Touch1.TouchPosition.ReadValue<Vector2>(), (float)context.time);
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
    private void LateUpdate()
    {
        

    }
    
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


