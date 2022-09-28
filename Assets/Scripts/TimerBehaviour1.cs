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
    [SerializeField] TextMeshProUGUI stunnedText;
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
    private PositionController positionController;
    // [SerializeField] private AttributeSettings attributeSettings;
    private bool activated = false;
    private bool stunned = false;
    public bool IsStealing { get; private set; }

    void OnDisable()
    {
        if (destroyOutOfBounds.ResetDefender)
        {
            getPositioned = false;
            coroutineStarted = false;
            ignoreCountdown = true;
            pressToBall = false;
            collide_count = 0;
            activated = false;
            stunned = false;
            positionController.ResetCoordinates();
            positionController.PositionIsEmptied();
            IsStealing = false;
            defenderAnim.speed = 1;
        }
    }

    void Awake()
    {
        destroyOutOfBounds = gameObject.GetComponent<DestroyOutOfBounds>();
        defenderShoulder = gameObject.GetComponent<DefenderShoulder>();
        defenderAnim = gameObject.GetComponent<Animator>();
        playerController = GameObject.Find("egoist").GetComponent<PlayerController>();
        // movementController = new MovementController(transform, attributeSettings);
        positionController = posCollider.GetComponent<PositionController>();
    }

    public void SetMovementController(MovementController mc)
    {
        movementController = mc;
    }


    public void ActivateTimerBehaviour()
    {
        activated = true;

        if (defenderShoulder.Falled)
        {
            // getPositioned = false;
            // dangerText.gameObject.SetActive(false);
            // defenderAnim.SetTrigger("Fall_to_right");
            // stunned = true;
            // defenderIsFalling = false; // bunu düzelt kötü oldu böyle. bir kere buraya girip çıkması için yaptın.
        }
        else if (stunned)
        {
            // stunned animasyonu veya texti gelebilir. Asl�nda gelmese daha iyi olur ��nk� bu oldu�unda a�a��daki olas�l�klara girmemesi i�in var ve bu yeterli. 
        }
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
            dangerText.gameObject.SetActive(false); // bunları burda yapmamanın bir yolunu bul
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
        if (Input.GetMouseButtonDown(0) && playerController.HaveBall && !IsPointerOverUIObject() && Time.timeScale != 0)
        {
            pressToBall = true;
        }

        if (stunned)
        {
            stunnedText.gameObject.SetActive(true);
            defenderAnim.speed = 0;

        }
        else
        {
            stunnedText.gameObject.SetActive(false);
            defenderAnim.speed = 1;
        }
    }

    public void Shake()
    {
        positionController.Shake();
        ignoreCountdown = true;
    }

    public void Stun()
    {
        StartCoroutine(StunCountdown());
        stunned = true;
        positionController.MoveDown();
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (activated)
        {
            if (col.gameObject.CompareTag(posCollider.tag) && collide_count == 0) // is defender getPositioned?
            {
                collide_count += 1;
                ignoreCountdown = true;
                getPositioned = true;
                positionController.PositionIsFilled();
            }
        }        
    }
        

    private void RunToBall() // bunları movementControllerdan çağır.
    {
        getPositioned = false;
        ignoreCountdown = true;
        movementController.MoveToAim(ball.transform.position); // burda sprintSpeed parametresi olmasa da olur.
        positionController.PositionIsEmptied();
    }

    private void RunToPosition() // bunları movementControllerdan çağır.
    {
        getPositioned = false;
        collide_count = 0;
        PositionCalculator();
        ignoreCountdown = true;
        movementController.MoveToAim(position);        
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

        /*
        if (IsStealing)
        {
            transform.position = position;
        }
        else
        {
            transform.SetPositionAndRotation(position, posCollider.transform.rotation);
        }*/

        // transform.Translate(onPositionSpeed * Time.deltaTime * Vector3.up, Space.World);
        /*if (rightDefenderAnim.GetCurrentAnimatorStateInfo(0).IsName("runback_right"))
        {
            rightDefenderAnim.SetBool("Runback", false); // bu ko�uldan kurtul
        }*/
    }
    

    IEnumerator StealCountdownRoutine()
    {
        coroutineStarted = true;
        ignoreCountdown = false; // this is for ignore countdown if a button pressed while counting.
        yield return new WaitForSeconds(Random.Range(1, 3));
        coroutineStarted = false;

        if (!ignoreCountdown)
        {
            Steal();
            ignoreCountdown = true;
        }

    }

    IEnumerator StunCountdown()
    {        
        // ignoreCountdown = false; // this is for ignore countdown if a button pressed while counting.
        yield return new WaitForSeconds(Random.Range(0.5f, 2));

        stunned = false;
        pressToBall = true;
        
        if (!ignoreCountdown)
        {
            
        }
    }


    public void Steal()
    {
        if (getPositioned)
        {
            IsStealing = true;
            ignoreCountdown = true;
            positionController.StealMovement();
            defenderAnim.SetTrigger("Steal");

            // transform.eulerAngles = new Vector3(0, 0, -60);
            /*
            Vector3 lookPoint = ball.transform.position;
            lookPoint.y += 1.3f;
            lookPoint.x += 0.1f;

            Vector3 lookdirection = lookPoint - transform.position;                           
            Quaternion rotation = Quaternion.LookRotation(Vector3.forward, -lookdirection);
            transform.rotation = rotation;
            */
            // transform.rotation = posCollider.transform.rotation;

            // posCollider.GetComponent<PositionController>().PositionIsEmptied();
        }
    }

    // I call this with animation event.
    private void ActionCompleted() 
    {
        if (IsStealing)
        {
            IsStealing = false;
            // positionController.ResetRotation();
        }
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
