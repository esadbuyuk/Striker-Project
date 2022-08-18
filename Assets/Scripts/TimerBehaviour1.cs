using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TimerBehaviour1 : MonoBehaviour, ITimerBehaviour
{
    private DefenderShoulder defenderShoulder;
    private bool getPositioned = false;
    [SerializeField] TextMeshProUGUI dangerText;
    private Animator defenderAnim;
    private PlayerController playerController;
    // private bool stunned = false;
    private int blockerForIf = 0;
    // private int stealTime; Asagıda
    private bool coroutineStarted = false;
    private bool ignoreCountdown = true;
    private bool pressToBall = false;
    [SerializeField] GameObject ball;
    // public Vector3 positionOffset;
    private Vector3 position;
    // private float onPositionSpeed = 2.5f;   
    // private PlayerController playerController;
    // private Quaternion firstRot;
    private DestroyOutOfBounds destroyOutOfBounds;
    // private SpriteRenderer mySpriteRenderer;
    [SerializeField] private GameObject posCollider;
    // private bool gameStopped = false;
    // private bool alreadyDone = false;
    private int collide_count = 0;
    //private bool timer = false;
    // public bool defenderIsFalling = false;
    private MovementController movementController;
    [SerializeField] private AttributeSettings attributeSettings;


    void OnDisable()
    {
        if (destroyOutOfBounds.ResetDefender)
        {
            getPositioned = false;
            coroutineStarted = false;
            ignoreCountdown = true;
            pressToBall = false;
            // stunned = false;           
            collide_count = 0;
        }
    }

    void Awake()
    {
        destroyOutOfBounds = gameObject.GetComponent<DestroyOutOfBounds>();
        defenderShoulder = gameObject.GetComponent<DefenderShoulder>();
        defenderAnim = gameObject.GetComponent<Animator>();
        playerController = GameObject.Find("egoist").GetComponent<PlayerController>();
        movementController = new MovementController(transform, attributeSettings);
    }


    public void ActivateTimerBehaviour()
    {
        if (defenderShoulder.Falled)
        {
            // getPositioned = false;
            // dangerText.gameObject.SetActive(false);
            // defenderAnim.SetTrigger("Fall_to_right");
            // stunned = true;
            // defenderIsFalling = false; // bunu düzelt kötü oldu böyle. bir kere buraya girip çıkması için yaptın.
        }/*
        else if (stunned)
        {            
            Debug.LogWarning("stunned");
            // stunned animasyonu veya texti gelebilir. Asl�nda gelmese daha iyi olur ��nk� bu oldu�unda a�a��daki olas�l�klara girmemesi i�in var ve bu yeterli. 
        }*/
        else if (!playerController.HaveBall) // buras� freeBalldu! // top oyuncuda de�ilken
        {
            blockerForIf = 0;
            getPositioned = false;
            dangerText.gameObject.SetActive(true);
            defenderAnim.ResetTrigger("Runback");
            defenderAnim.SetTrigger("Sprint");
            RunToBall();
        }
        else if (pressToBall) // top oyuncuda ve topa bask� yapmak istedi�inde
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

            getPositioned = false;
            dangerText.gameObject.SetActive(true);
            defenderAnim.ResetTrigger("Runback");
            defenderAnim.SetTrigger("Sprint");
            RunToBall();
        }
        else if (!getPositioned) // top oyuncuda ve pozisyon al�nmad���nda yani pozisyona ko�mak istedi�inde
        {
            dangerText.gameObject.SetActive(false);
            collide_count = 0;
            defenderAnim.ResetTrigger("Runback");
            defenderAnim.SetTrigger("Sprint");
            RunToPosition();

            // destroyOutOfBounds.ResetDefender = false; // bunu kontrol etmedin !
        }
        else if (getPositioned) // top oyuncuda ve pozisyon al�nd���nda
        {
            dangerText.gameObject.SetActive(false);
            defenderAnim.ResetTrigger("Sprint");
            defenderAnim.SetTrigger("Runback");
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
            */
            if (!coroutineStarted) // countdown ba�lamad�ysa
            {
                StartCoroutine(StealCountdownRoutine());
            }
        }

        // Inputs:       
        if (Input.GetMouseButtonDown(0) && playerController.HaveBall && !IsPointerOverUIObject() && Time.timeScale == 1)
        {
            pressToBall = true;
        }
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag(posCollider.tag) && collide_count == 0) // is defender getPositioned? burda görünmeyen && timer? var.
        {
            collide_count += 1;
            ignoreCountdown = true;
            getPositioned = true;
        }
    }

    private void RunToBall() // bunları movementControllerdan çağır.
    {
        getPositioned = false;
        ignoreCountdown = true;
        movementController.MoveToAim(ball.transform.position, attributeSettings.SprintSpeed);
    }


    private void RunToPosition() // bunları movementControllerdan çağır.
    {
        getPositioned = false;
        collide_count = 0;
        PositionCalculator();
        ignoreCountdown = true;
        movementController.MoveToAim(position, attributeSettings.SprintSpeed);        
    }

    private Vector3 PositionCalculator()
    {
        position = posCollider.transform.position;
        position.z = transform.position.z;
        return position;
    }


    private void StayOnPosition()
    {
        PositionCalculator();
        transform.SetPositionAndRotation(position, posCollider.transform.rotation);

        // transform.Translate(onPositionSpeed * Time.deltaTime * Vector3.up, Space.World);
        /*if (rightDefenderAnim.GetCurrentAnimatorStateInfo(0).IsName("runback_right"))
        {
            rightDefenderAnim.SetBool("Runback", false); // bu ko�uldan kurtul
        }*/
    }


    IEnumerator StealCountdownRoutine()
    {
        ignoreCountdown = false; // this is for ignore countdown if a button pressed while counting.

        coroutineStarted = true;
        yield return new WaitForSeconds(Random.Range(1, 3));
        coroutineStarted = false;

        if (!ignoreCountdown)
        {
            getPositioned = false;
            defenderAnim.SetTrigger("Steal_to_ball");
            transform.eulerAngles = new Vector3(0, 0, -28);
            pressToBall = true;
            ignoreCountdown = true;
            // stunned = true;
        }

    }


    // Bu fonksiyonu playerControllerdan çağırman gerek!
    public void Steal() // bunları movementControllerdan çağır.
    {
        if (getPositioned)
        {
            // stunned = true;
            ignoreCountdown = true;
            getPositioned = false;

            // transform.eulerAngles = new Vector3(0, 0, 0);

            Vector3 lookdirection = ball.transform.position - transform.position;//normalized gelebilir.                           
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -lookdirection);

            transform.rotation = rotation;

            defenderAnim.SetTrigger("Steal");
            defenderAnim.SetBool("Runback", false);
        }
    }


    // Animation event ile cagırılıyor.
    private void StealFinished()
    {
        getPositioned = false;
        pressToBall = true;
        // stunned = false;
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
}
