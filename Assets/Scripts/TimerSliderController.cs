using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class TimerSliderController : MonoBehaviour
{/*
    private Animator leftDefenderAnim;
    public bool getPositionedL = false; // bu static idi bozuktu değiştirdin.
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
    // private bool gameStopped = false;
    private bool alreadyDone = false;
    public TextMeshProUGUI tipText3;
    public TextMeshProUGUI tipClick3;
    private DefenderTypeChooser defenderTypeChooser;


    void OnDisable()
    {
        if (destroyOutOfBounds.resetLeft)
        {
            leftDefenderAnim.SetTrigger("Sprint");

            playerController.leftDfalling = false;
            getPositionedL = false;
            coroutineStarted = false;
            ignoreCountdown = true;
            pressToBall = false;
            stunned = false;            
            collide_count = 0;
            dangerText.gameObject.SetActive(false);

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

        playerController = egoist.GetComponent<PlayerController>();
        destroyOutOfBounds = gameObject.GetComponent<DestroyOutOfBounds>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        defenderTypeChooser = gameObject.GetComponent<DefenderTypeChooser>();
    }

    void Start()
    {
        getPositionedL = false;
        // firstRot = transform.rotation;        

        if (mySpriteRenderer != null)
        {
            // flip the sprite
            mySpriteRenderer.flipX = false;
            mySpriteRenderer.flipY = false;
        }
    }



    void LateUpdate()
    {
        if (defenderTypeChooser.slider)
        {
            SliderBehaviour();
        }
        else if (defenderTypeChooser.timer)
        {
            TimerBehaviour();
        }
    }
    


    private void SliderBehaviour()
    {
        DistanceToBallCalculator();

        dangerText.gameObject.SetActive(true);

        if (distanceToBall.y < 3.5)
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
            getPositionedL = false;
            dangerText.gameObject.SetActive(false);
            leftDefenderAnim.SetTrigger("Fall_to_right");
            stunned = true;
            playerController.leftDfalling = false;
        }
        else if (stunned)
        {
            getPositionedL = false;
            dangerText.gameObject.SetActive(false);
            Debug.Log("stunned");
            // stunned animasyonu veya texti gelebilir. Asl�nda gelmese daha iyi olur ��nk� bu oldu�unda a�a��daki olas�l�klara girmemesi i�in var ve bu yeterli.             
        }
        else if (!PlayerController.haveBall) // buras� freeBalldu! // top oyuncuda de�ilken
        {
            blockerForIf = 0;
            getPositionedL = false;
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

            getPositionedL = false;
            dangerText.gameObject.SetActive(true);
            leftDefenderAnim.ResetTrigger("Runback");
            leftDefenderAnim.SetTrigger("Sprint");
            RunToBall();
        }
        else if (!getPositionedL) // top oyuncuda ve pozisyon al�nmad���nda
        {
            dangerText.gameObject.SetActive(false);
            leftDefenderAnim.ResetTrigger("Runback");
            leftDefenderAnim.SetTrigger("Sprint");
            RunToPosition();

            destroyOutOfBounds.resetLeft = false;
        }
        else if (getPositionedL) // top oyuncuda ve pozisyon al�nd���nda
        {
            dangerText.gameObject.SetActive(false);
            Debug.Log("getPositionedL = true");
            leftDefenderAnim.ResetTrigger("Sprint");
            leftDefenderAnim.SetTrigger("Runback");
            StayOnPosition();

            if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
            {
                if (!alreadyDone)
                {
                    StopGame();
                    tipText2.gameObject.SetActive(true);
                    alreadyDone = true;
                }
                alreadyDone = false;
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
            if (!coroutineStarted) // countdown baslamadiysa
            {
                StartCoroutine(StealCountdownRoutine());
            }
        }


        // Inputs:       
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject() && Time.timeScale == 1) // i dont have any idea about what i did here
        {
            pressToBall = true;

            if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
            {
                tipClick3.gameObject.SetActive(false);
            }
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
            getPositionedL = false;
            leftDefenderAnim.SetTrigger("Steal_to_ball");
            transform.eulerAngles = new Vector3(0, 0, 28);
            pressToBall = true;
            ignoreCountdown = true;
            stunned = true;
        }

    }

    public IEnumerator StunCountdownRoutine(float second)
    {
        if (getPositionedL)
        {
            stunned = true;
            yield return new WaitForSeconds(second);
            stunned = false;
            // pressToBall = true;
            getPositionedL = true;
        }

    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(posCollider.tag) && collide_count == 0 && defenderTypeChooser.timer) // is defender getPositionedL?
        {
            ignoreCountdown = true;
            getPositionedL = true;
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
        getPositionedL = false;
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

        Vector3 lookdirection = ball.transform.position - transform.position;//normalized gelebilir.                           
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
        if (getPositionedL)
        {
            stunned = true;
            ignoreCountdown = true;
            getPositionedL = false;

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

    }  // animation event ile �a�r�l�yor.


    public void MiddleOfSteal() // bu private d� denemek i�in public yapt�n!
    {
        if (SceneManager.GetActiveScene().name == "SurvivalTutorial")
        {
            StopGame();
            tipText3.gameObject.SetActive(true);
            tipClick3.gameObject.SetActive(true);
            alreadyDone = true;
        }

    }  // animation event ile �a�r�l�yor.


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
        // gameStopped = true;
    }


    private void ErrorDetector()
    {
        Debug.Log("animation called!");
    }*/
}
