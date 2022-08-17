using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class TutorialLDefController : MonoBehaviour
{/*
    private Animator leftDefenderAnim;
    // public static bool getPositionedL = false;
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
    public TextMeshProUGUI tipText2;
    // private bool gameStopped = false; // I prefer to do this on GameManager script
    private bool alreadyDone = false;
    public TextMeshProUGUI tipText3;

    // private LDefenderController lDefenderController;



    void OnDisable()
    {
        if (destroyOutOfBounds.resetLeft)
        {
            leftDefenderAnim.SetTrigger("Sprint");

            playerController.leftDfalling = false;
            LDefenderController.getPositionedL = false;
            coroutineStarted = false;
            ignoreCountdown = true;
            pressToBall = false;
            stunned = false;
            slider = false;
            timer = false;
            collide_count = 0;
            dangerText.gameObject.SetActive(false);
            alreadyDone = false;

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
        leftDefenderAnim = GetComponent<Animator>();

        ball = GameObject.Find("ball");
        egoist = GameObject.Find("egoist");

        gameObject.GetComponent<LDefenderController>();
        playerController = egoist.GetComponent<PlayerController>();
        destroyOutOfBounds = gameObject.GetComponent<DestroyOutOfBounds>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Start()
    {
        LDefenderController.getPositionedL = false;
        // firstRot = transform.rotation;        

        DefenderTypeChooser();

        if (mySpriteRenderer != null)
        {
            // flip the sprite
            mySpriteRenderer.flipX = false;
            mySpriteRenderer.flipY = false;
        }
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
    }


    private void DefenderTypeChooser()
    {
        defenderType = Random.Range(1, 4); 

        if (defenderType == 0) // 0 -> 1 yapilcak
        {
            slider = true;
        }
        else
        {
            timer = true;
            stealTime = Random.Range(1, 3);
            Debug.Log("steal time for LDef: " + stealTime);
        }
    }


    private void SliderBehaviour()
    {
        DistanceToBallCalculator();

        dangerText.gameObject.SetActive(true);

        if (distanceToBall.y < 3)
        {
            leftDefenderAnim.SetTrigger("Tackle");
            Tackle();
        }
        else if (!PlayerController.haveBall) // top oyuncuda de�ilken
        {
            leftDefenderAnim.ResetTrigger("Runback");
            leftDefenderAnim.SetTrigger("Sprint");
            RunToBall();
        }
        else
        {
            leftDefenderAnim.ResetTrigger("Runback");
            leftDefenderAnim.SetTrigger("Sprint");
            RunForTackle();
        }
    }


    private void TimerBehaviour()
    {
        if (playerController.leftDfalling)
        {
            LDefenderController.getPositionedL = false;
            dangerText.gameObject.SetActive(false);
            leftDefenderAnim.SetTrigger("Fall_to_right");
            stunned = true;
            playerController.leftDfalling = false;
        }
        else if (stunned)
        {

            LDefenderController.getPositionedL = false;
            dangerText.gameObject.SetActive(false);
            Debug.Log("stunnedL" + playerController.leftDfalling);
            // stunned animasyonu veya texti gelebilir. Asl�nda gelmese daha iyi olur ��nk� bu oldu�unda a�a��daki olas�l�klara girmemesi i�in var ve bu yeterli.             
        }
        else if (!PlayerController.haveBall) // buras� freeBalldu! // top oyuncuda de�ilken
        {
            blockerForIf = 0;
            LDefenderController.getPositionedL = false;
            dangerText.gameObject.SetActive(true);
            leftDefenderAnim.ResetTrigger("Runback");
            leftDefenderAnim.SetTrigger("Sprint");
            RunToBall();
        }
        else if (pressToBall) // top oyuncuda ve topa bask� yapmak istedi�inde. (Mathf.Abs(DistanceToBallCalculator().y) < 4) && (Mathf.Abs(DistanceToBallCalculator().x) < 4) && 
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

            LDefenderController.getPositionedL = false;
            dangerText.gameObject.SetActive(true);
            leftDefenderAnim.ResetTrigger("Runback");
            leftDefenderAnim.SetTrigger("Sprint");
            RunToBall();
        }
        else if (!LDefenderController.getPositionedL) // top oyuncuda ve pozisyon al�nmad���nda
        {
            dangerText.gameObject.SetActive(false);
            leftDefenderAnim.ResetTrigger("Runback");
            leftDefenderAnim.SetTrigger("Sprint");
            RunToPosition();

            destroyOutOfBounds.resetLeft = false;
        }
        else if (LDefenderController.getPositionedL) // top oyuncuda ve pozisyon al�nd���nda
        {            
            dangerText.gameObject.SetActive(false);
            Debug.Log("getPositionedL = true");
            leftDefenderAnim.ResetTrigger("Sprint");
            leftDefenderAnim.SetTrigger("Runback");
            StayOnPosition();

            
            if (!alreadyDone)
            {
                StopGame();
                tipText2.gameObject.SetActive(true);
                alreadyDone = true;
            }


            /*
            // Inputs:
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Steal();
            }

            if (Input.GetKeyDown(KeyCode.D))
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
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject() && Time.timeScale == 1) // i dont have any idea about what i did here
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
            LDefenderController.getPositionedL = false;
            leftDefenderAnim.SetTrigger("Steal_to_ball");
            transform.eulerAngles = new Vector3(0, 0, 28);
            pressToBall = true;
            ignoreCountdown = true;
            stunned = true;
        }

    }

    public IEnumerator StunCountdownRoutine(float second)
    {
        if (LDefenderController.getPositionedL)
        {
            stunned = true;
            yield return new WaitForSeconds(second);
            stunned = false;
            // pressToBall = true;
            LDefenderController.getPositionedL = true;
        }

    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("position collider L") && collide_count == 0 && timer) // is defender getPositionedL?
        {
            ignoreCountdown = true;
            LDefenderController.getPositionedL = true;
            collide_count += 1;
            // Debug.Log("position collider activated");
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
        LDefenderController.getPositionedL = false;
        ignoreCountdown = true;

        ballPosition.z = transform.position.z;
        ballPosition.x = ball.transform.position.x + 0.0f;
        ballPosition.y = ball.transform.position.y + 0.3f;
        lookDirection = ballPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
    }


    private void RunToPosition()
    {
        // getPositionedL = false;

        collide_count = 0;
        PositionCalculator();
        ignoreCountdown = true;


        Vector3 lookdirection = position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookdirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);

        if (transform.position.x == PositionCalculator().x && transform.position.y == PositionCalculator().y)
        {

        }
    }


    private void RunToBall()
    {
        ignoreCountdown = true;

        Vector3 lookdirection = ball.transform.position - transform.position; //normalized gelebilir.                           
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookdirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
    }


    private void StayOnPosition()
    {
        PositionCalculator();

        transform.SetPositionAndRotation(position, Quaternion.Euler(0, 0, 45));

        // transform.rotation = Quaternion.Euler(0, 0, 45);
        // transform.Translate(onPositionSpeed * Time.deltaTime * Vector3.up, Space.World);
    }

    private void Tackle()
    {
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
        ignoreCountdown = true;
    }


    public void Steal()
    {
        if (LDefenderController.getPositionedL)
        {
            stunned = true;
            LDefenderController.getPositionedL = false;
            ignoreCountdown = true;

            // transform.Rotate(0, 0, 0);
            // transform.eulerAngles = new Vector3(0, 0, 0);

            Vector3 lookdirection = ball.transform.position - transform.position;//normalized gelebilir.                           
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -lookdirection);

            transform.rotation = rotation;

            leftDefenderAnim.SetTrigger("Steal");            
        }
    }


    public void StealFinished() // bu private d� denemek i�in public yapt�n!
    {
        pressToBall = true;
        stunned = false;
    } // animation event ile �a�r�l�yor.

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


    private void StopGame()
    {
        Time.timeScale = 0;
        // gameStopped = true; // I prefer to do this on GameManager script
    }


    private void ErrorDetector()
    {
        Debug.Log("animation called!");
    }*/
}
