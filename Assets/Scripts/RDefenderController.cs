using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class RDefenderController : MonoBehaviour
{/*
    private Animator rightDefenderAnim;
    public static bool getPositionedR = false;
    private int stealTime;
    private bool coroutineStarted = false;
    private bool ignoreCountdown = true;
    private bool pressToBall = false;
    private GameObject ball;
    private float sprintSpeed = 5.0f;
    private GameObject egoist;
    // public Vector3 positionOffset;
    private Vector3 position;
    private Vector3 ballPosition;
    public bool stunned = false;
    private int defenderType;
    private bool slider = false;
    private bool timer = false;
    private Vector3 distanceToEgoist;
    private Vector3 distanceToBall;
    private Vector3 lookDirection;
    // private float onPositionSpeed = 2.5f;
    private int collide_count = 0;
    private PlayerController playerController;
    // private Quaternion firstRot;
    private DestroyOutOfBounds destroyOutOfBounds;
    private SpriteRenderer mySpriteRenderer;
    public GameObject posCollider;
    public TextMeshProUGUI dangerText;
    private int blockerForIf = 0;
    public TextMeshProUGUI tipText5;
    // private bool gameStopped = false;
    // private bool alreadyDone = false;


    void OnDisable()
    {
        if (destroyOutOfBounds.resetRight)
        {
            rightDefenderAnim.SetTrigger("Sprint");
            playerController.rightDfalling = false;
            getPositionedR = false;
            coroutineStarted = false;
            ignoreCountdown = true;
            pressToBall = false;
            stunned = false;
            slider = false;
            timer = false;
            collide_count = 0;
            dangerText.gameObject.SetActive(false);

            DefenderTypeChooser();

            if (mySpriteRenderer != null)
            {
                // flip the sprite
                mySpriteRenderer.flipX = false;
                mySpriteRenderer.flipY = false;
            }
            
        }

    }

    void OnEnable()
    {
        
    }


    void Awake()
    {
        rightDefenderAnim = GetComponent<Animator>();

        ball = GameObject.Find("ball");
        egoist = GameObject.Find("egoist");

        playerController = egoist.GetComponent<PlayerController>();
        destroyOutOfBounds = gameObject.GetComponent<DestroyOutOfBounds>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }


    void Start()
    {
        getPositionedR = false;
        // firstRot = transform.rotation;

        if (mySpriteRenderer != null)
        {
            // flip the sprite
            mySpriteRenderer.flipX = false;
            mySpriteRenderer.flipY = false;
        }

        DefenderTypeChooser();
    }



    void LateUpdate()
    {

        if (slider)
        {
            SliderBehaviour();
        }
        else if (timer)
        {
            TimerBehaviour();
        }
    } // leftdefender da buras� lateupdate.



    private void DefenderTypeChooser()
    {
        if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
        {
            slider = true;
        }
        else
        {
            defenderType = Random.Range(1, 4);  // 2 1 yap�lcak

            if (defenderType == 1)
            {
                slider = true;
            }
            else
            {
                timer = true;
                stealTime = Random.Range(1, 3);
                Debug.Log("steal time for RDef: " + stealTime);
            }
        }
    }


    private void SliderBehaviour()
    {
        DistanceToBallCalculator();

        dangerText.gameObject.SetActive(true);

        if (distanceToBall.y < 3.5) 
        {
            rightDefenderAnim.SetTrigger("Tackle");
            Tackle();            
        }
        else if (!PlayerController.haveBall) // top oyuncuda de�ilken
        {
            rightDefenderAnim.ResetTrigger("Runback");
            rightDefenderAnim.SetTrigger("Sprint");
            RunToBall();
        }
        else       
        {
            rightDefenderAnim.ResetTrigger("Runback");
            rightDefenderAnim.SetTrigger("Sprint");
            RunForTackle();
        }
    }


    private void TimerBehaviour() // stunned i�in ayr� yazma �ekline ge�ir leftDefenderdan farkl� buras�
    {
        if (playerController.rightDfalling)
        {
            getPositionedR = false;
            dangerText.gameObject.SetActive(false);
            rightDefenderAnim.SetTrigger("Fall_to_right");
            stunned = true;
            playerController.rightDfalling = false;
        }
        else if (stunned)
        {
            getPositionedR = false;
            dangerText.gameObject.SetActive(false);
            Debug.Log("stunnedR" + playerController.rightDfalling);
            // stunned animasyonu veya texti gelebilir. Asl�nda gelmese daha iyi olur ��nk� bu oldu�unda a�a��daki olas�l�klara girmemesi i�in var ve bu yeterli. 
        }
        else if (!PlayerController.haveBall) // buras� freeBalldu! // top oyuncuda de�ilken
        {
            blockerForIf = 0;
            getPositionedR = false;
            dangerText.gameObject.SetActive(true);
            rightDefenderAnim.ResetTrigger("Runback");
            rightDefenderAnim.SetTrigger("Sprint");
            RunToBall();
        }
        else if(pressToBall) // top oyuncuda ve topa bask� yapmak istedi�inde
        {
            if (blockerForIf == 0 && transform.position.y - ball.transform.position.y < 6)
            {
                pressToBall = true;
                blockerForIf += 1;
            }
            else if (blockerForIf == 0)
            {
                pressToBall = false;
                blockerForIf += 1;
            }

            getPositionedR = false;
            dangerText.gameObject.SetActive(true);
            rightDefenderAnim.ResetTrigger("Runback");
            rightDefenderAnim.SetTrigger("Sprint");
            RunToBall();
        }
        else if (!getPositionedR) // top oyuncuda ve pozisyon al�nmad���nda yani pozisyona ko�mak istedi�inde
        {
            dangerText.gameObject.SetActive(false);
            collide_count = 0;
            rightDefenderAnim.ResetTrigger("Runback");
            rightDefenderAnim.SetTrigger("Sprint");
            RunToPosition();

            destroyOutOfBounds.resetRight = false;
        }       
        else if (getPositionedR) // top oyuncuda ve pozisyon al�nd���nda
        {
            dangerText.gameObject.SetActive(false);
            rightDefenderAnim.ResetTrigger("Sprint");
            rightDefenderAnim.SetTrigger("Runback");
            StayOnPosition();

            // Inputs:
            /*
            if (Input.GetKeyDown(KeyCode.E))
            {
                Steal();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                // StartCoroutine(StunCountdownRoutine(0.5f));                
            }
            *//*
            if (!coroutineStarted) // countdown ba�lamad�ysa
            {
                StartCoroutine(StealCountdownRoutine());
            }
        }

       
        // Inputs:       
        if (Input.GetMouseButtonDown(0) && PlayerController.haveBall && !IsPointerOverUIObject() && Time.timeScale == 1)
        {       
            pressToBall = true;
           
        }

    }


    IEnumerator StealCountdownRoutine()
    {
        ignoreCountdown = false; // this is for ignore countdown if a button pressed while counting.

        coroutineStarted = true;
        yield return new WaitForSeconds(stealTime);
        coroutineStarted = false;

        if (!ignoreCountdown)
        {
            getPositionedR = false;
            rightDefenderAnim.SetTrigger("Steal_to_ball");
            transform.eulerAngles = new Vector3(0, 0, -28);
            pressToBall = true;
            ignoreCountdown = true;                        
            stunned = true;            
        }

    }

    public IEnumerator StunCountdownRoutine(float second)
    {
        if (getPositionedR)
        {
            stunned = true;
            yield return new WaitForSeconds(second);
            stunned = false;
            // pressToBall = true;
            getPositionedR = true;
        }

    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("position collider R") && collide_count == 0 && timer) // is defender getPositioned?
        {
            collide_count += 1;
            ignoreCountdown = true;
            getPositionedR = true;
        }
    }

    private Vector3 DistanceToEgoistCalculator()
    {
        distanceToEgoist = transform.position - egoist.transform.position;
        return distanceToEgoist;
    }


    private Vector3 DistanceToBallCalculator()
    {
        distanceToBall = transform.position - ball.transform.position;
        return distanceToBall;
    }


    private Vector3 PositionCalculator()
    {
        position = posCollider.transform.position;
        position.z = transform.position.z;
        return position;
    }


    private void RunForTackle()
    {
        getPositionedR = false;

        ignoreCountdown = true;

        ballPosition.z = transform.position.z;
        ballPosition.x = ball.transform.position.x + 0.0f;
        ballPosition.y = ball.transform.position.y + 1.2f;
        lookDirection = ballPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
    }


    private void RunToPosition()
    {
        getPositionedR = false;

        collide_count = 0;
        PositionCalculator();
        ignoreCountdown = true;

        Vector3 lookdirection = position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookdirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
    }


    private void RunToBall()
    {
        getPositionedR = false;
        
        ignoreCountdown = true;

        Vector3 lookdirection = ball.transform.position - transform.position; // normalized gelebilir.                           
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookdirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
    }


    private void StayOnPosition()
    {
        PositionCalculator();
        // transform.rotation = Quaternion.Euler(0, 0, -45);
        // transform.position = position;
        transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, -45));

        // transform.Translate(onPositionSpeed * Time.deltaTime * Vector3.up, Space.World);
        /*if (rightDefenderAnim.GetCurrentAnimatorStateInfo(0).IsName("runback_right"))
        {
            rightDefenderAnim.SetBool("Runback", false); // bu ko�uldan kurtul
        }*//*
    }


    private void Tackle()
    {
        getPositionedR = false;

        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);        
        ignoreCountdown = true;
    }


    public void Steal()
    { 
        if (getPositionedR)
        {    
            stunned = true;            
            ignoreCountdown = true;
            getPositionedR = false;

            // transform.eulerAngles = new Vector3(0, 0, 0);

            Vector3 lookdirection = ball.transform.position - transform.position;//normalized gelebilir.                           
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -lookdirection);

            transform.rotation = rotation;
       
            rightDefenderAnim.SetTrigger("Steal");
            rightDefenderAnim.SetBool("Runback", false);
        }
    }

    private void StealFinished() 
    {
        getPositionedR = false;
        pressToBall = true;
        stunned = false;
    } // animation event ile cagırılıyor.

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

    public void MiddleOfTackle()
    {
        if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
        {              
            StopGame();
            tipText5.gameObject.SetActive(true);
            // alreadyDone = true;            
        }
    }

    private void ErrorDetector()
    {
        Debug.Log("animation called!");
    }

    private void StopGame()
    {
        Time.timeScale = 0;
        // gameStopped = true;
    }
*/
}
