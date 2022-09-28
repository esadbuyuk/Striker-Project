using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public bool HaveBall { get; private set; } = true;

    [SerializeField] private GameObject ball;
    [SerializeField] private GameObject goal;
    [SerializeField] private Button fakeButton;
    [SerializeField] private AttributeSettings attributeSettings;
    [SerializeField] private SkillSettings skillSettings;
    [SerializeField] private GameObject leftPositon;
    [SerializeField] private GameObject rightPositon;
    private PositionController rightPosController;
    private PositionController leftPosController;
    private Animator playerAnim;
    private Quaternion rotationToUp;
    private DefenderShoulder defenderShoulder;
    [SerializeField] private GameObject rightDef;
    [SerializeField] private GameObject leftDef;
    private ITimerBehaviour lDefenderController;
    private ITimerBehaviour rDefenderController;
    private BallController ballController;
    private GameObject collideDef;
    private MovementController movementController;
    private SpriteFlipper spriteFlipper;
    private Vector3 aim;
    private bool shoulderContestOn = false;
    private bool shiftingToRight;
    private bool shiftingToLeft;
    private bool action;
    [SerializeField] private float dribbleSpeed = 2.5f;

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        ballController = ball.GetComponent<BallController>();
        movementController = new MovementController(transform, attributeSettings);
        spriteFlipper = new SpriteFlipper(transform);
        leftPosController = leftPositon.GetComponent<PositionController>();
        rightPosController = rightPositon.GetComponent<PositionController>();
        lDefenderController = leftDef.GetComponent<ITimerBehaviour>();
        rDefenderController = rightDef.GetComponent<ITimerBehaviour>();
    }


    void Start()
    {
        HaveBall = true;       
        rotationToUp = transform.rotation;
        spriteFlipper.FlipToRight();
    }


    void Update() // update yap�lcak!
    {
        EgoistInputs(); // bunun için ayrı bir class açılabilir ama vakit alıcağından şuanlık tercih etmiyorum.

        if (action)
        {

        }
        else if (HaveBall)
        {
            Dribble();
        }
        else
        {
            Sprint();
        }
    }


    private void EgoistInputs() // animasyonlarda loop time ı kapa!
    {
        if (Input.GetKeyDown(KeyCode.A) && HaveBall && !action)
        {
            action = true;

            #region Smart Shifting 

            // her iki savunmadan biri steal animasyonundaysa o tarafa kayılır ve dodge + stun olur.
            // aynı anda sağ ve sol pozisonlar doluysa seçim rastgele yapılır.
            // sol pozisyon boşsa sola kayılır.
            // sağ pozisyon boşsa sağa kayılır.
            // ikiside boşsa seçim rastgele yapılır.

            if (lDefenderController.IsStealing)
            {
                lDefenderController.Stun();
                ShiftToLeft();
            }
            else if (rDefenderController.IsStealing)
            {
                rDefenderController.Stun();
                ShiftToRight();
            }
            else if (rightPosController.IsPositionFull() && leftPosController.IsPositionFull())
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        ShiftToRight();
                        break;
                    case 2:
                        ShiftToLeft();
                        break;                    
                }
            }
            else if (!rightPosController.IsPositionFull() && !leftPosController.IsPositionFull())
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        ShiftToRight();
                        break;
                    case 2:
                        ShiftToLeft();
                        break;
                }
            }
            else if (rightPosController.IsPositionFull())
            {
                ShiftToLeft();
            }
            else if (leftPosController.IsPositionFull())
            {
                ShiftToRight();
            }
            #endregion
        }

        if (Input.GetKeyDown(KeyCode.Q) && HaveBall && !action) // IsStealing true iken stun yiyebilir burda da.
        {
            #region Smart Faking 

            if (rightPosController.IsPositionFull() && leftPosController.IsPositionFull())
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        FakeRight();
                        rDefenderController.Shake();
                        break;
                    case 2:
                        FakeLeft();
                        lDefenderController.Shake();
                        break;
                }
            }
            else if (!rightPosController.IsPositionFull() && !leftPosController.IsPositionFull())
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        FakeRight();
                        rDefenderController.Shake();
                        break;
                    case 2:
                        FakeLeft();
                        lDefenderController.Shake();
                        break;
                }
            }
            else if (rightPosController.IsPositionFull())
            {
                FakeRight();
                rDefenderController.Shake();
            }
            else if (leftPosController.IsPositionFull())
            {
                FakeLeft();
                lDefenderController.Shake();
            }
            #endregion
        }

        /*
        if (RDefenderController.getPositionedR || LDefenderController.getPositionedL) // sag savunma pozisyon almamis ise saga kayma yapilabilsin.
        {
            fakeButton.interactable = true;
        }
        else
        {
            fakeButton.interactable = false;
        }

        if (Input.GetKeyDown(KeyCode.A)) // haveBall kontrolü yapılabilir daha sonra bak buraya!
        {
            if (lDefenderController.stunned && !RDefenderController.getPositionedR) // sol savunma stunlanm�� ve sa� savunma pozisyon almam�� ise sa�a kayma yap�labilsin.
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                playerAnim.SetTrigger("dodge"); // Go_right olarak de�i�tirilip blend tree yap�l�cak.

                if (shoot) // bunu BallControllerdan çağırman gerekirdi.
                {
                    moveForward.spinToRight = true;
                }
            }
            else if (rDefenderController.stunned && !LDefenderController.getPositionedL) // sa� savunma stunlanm�� ve sol savunma pozisyon almam�� ise sola kayma yap�labilsin.
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                playerAnim.SetTrigger("dodge");

                if (shoot) // bunu BallControllerdan çağırman gerekirdi.
                {
                    moveForward.spinToLeft = true;
                    // ball.transform.Translate(0.2f, 0, 0);
                    // Debug.Log("sola Falso verildi");
                }
            }
            else if (!RDefenderController.getPositionedR) // sa� savunma pozisyon almam�� ise sa�a kayma yap�labilsin.
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                playerAnim.SetTrigger("dodge"); // Go_right olarak de�i�tirilip blend tree yap�l�cak.

                if (shoot) // bunu BallControllerdan çağırman gerekirdi.
                {
                    moveForward.spinToRight = true;
                }
            }
            else if (!LDefenderController.getPositionedL) 
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                playerAnim.SetTrigger("dodge");

                if (shoot) // bunu BallControllerdan çağırman gerekirdi.
                {
                    moveForward.spinToLeft = true;
                    // ball.transform.Translate(0.2f, 0, 0);
                    // Debug.Log("sola Falso verildi");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) // burda skills state machine e dodge triggerı ile giriyor onu düzeltmen gerekebilir.
        { 
            if (RDefenderController.getPositionedR)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                playerAnim.SetTrigger("fake");
                rDefenderController.Steal();

                if (shoot) // bunu BallControllerdan çağırman gerekirdi.
                {
                   moveForward.spinToRight = true;
                }             
            }   
            else if (LDefenderController.getPositionedL)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                playerAnim.SetTrigger("fake");
                lDefenderController.Steal();
                // transform.localScale = new Vector3(-1f, 1f, 1f);

                if (shoot) // bunu BallControllerdan çağırman gerekirdi.
                {
                    moveForward.spinToLeft = true;

                }
            }
        }*/


        if (Input.GetMouseButtonDown(0) && HaveBall && !IsPointerOverUIObject() && Time.timeScale != 0)
        {
            AimCalculator();
            if (IsAimOnGoal())
            {
                Shoot();
            }
            else
            {
                Pass();
            }
        }
    }

    #region ShoulderContest
    private void OnTriggerStay2D(Collider2D col) // ShoulderContest
    {
        if (col.gameObject.CompareTag("defender_body")) // defenderlarin isTrigger acik.
        {
            if (!HaveBall && !shoulderContestOn)
            {
                // animation event works here!                

                collideDef = col.gameObject;
                defenderShoulder = collideDef.GetComponent<DefenderShoulder>();

                if (IsDefOnLeft())
                {
                    // defender soldaysa                    
                    playerAnim.SetFloat("def_onleft", 1f);  // bunu scale kullanarak ayarlarsın ama forma numarasını kaldırman gerekicek.Böyle kalırsa daha iyi gibi.                 
                }
                else
                {
                    // defender sa�daysa
                    playerAnim.SetFloat("def_onleft", 0f);
                }

                playerAnim.ResetTrigger("sprint");
                playerAnim.SetBool("shoulder_contest", true);
                shoulderContestOn = true;
            }
        }
    }
    #endregion

    #region Take ball, Avoid
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("tackle collider"))
        {
            if (!HaveBall && !shoulderContestOn)
            {
                playerAnim.SetTrigger("avoid");
            }
        }
        
        if (col.gameObject.CompareTag("ball"))
        {
            HaveBall = true;

            if (shoulderContestOn == true)
            {
                playerAnim.SetBool("shoulder_contest", false);
                shoulderContestOn = false;
                defenderShoulder.EndShoulderContest();
            }
        }
    }
    #endregion

    public void ActionCompleted() // ı call this with animation event.
    {
        action = false;
        
        if (shiftingToRight)
        {
            ShiftDefenderPosition(rightPosController);
            ShiftDefenderPosition(leftPosController);
            shiftingToRight = false;
        }
        else if (shiftingToLeft)
        {
            ShiftDefenderPosition(leftPosController);
            ShiftDefenderPosition(rightPosController);
            shiftingToLeft = false;
        }
    }

    public void FreezeDefenderPosition(PositionController positionController)
    {
        positionController.Freeze();
    }

    public void ShiftDefenderPosition(PositionController positionController)
    {
        positionController.Shift();
    }

    public void PassBall() // ı call this with animation event.
    {
        ballController.Passed(aim);
        HaveBall = false;
    }

    public void ShootBall() // ı call this with animation event.
    {
        // buraya bi shootSpeed parametresi gönder
        ballController.Shooted(aim);
        HaveBall = false;
    }

    public void SetDefInActive()
    {
        if (shoulderContestOn == true)
        {
            defenderShoulder.StartShoulderContest(IsDefOnLeft());
        }
    }  

    private void FakeRight()
    {
        action = true;
        transform.localScale = new Vector3(1f, 1f, 1f);
        playerAnim.SetTrigger("fake");
    }

    private void FakeLeft()
    {
        action = true;
        transform.localScale = new Vector3(-1f, 1f, 1f);
        playerAnim.SetTrigger("fake");
    }

    private void ShiftToRight()
    {
        shiftingToRight = true;
        action = true;
        transform.localScale = new Vector3(1f, 1f, 1f);
        playerAnim.SetTrigger("shift");
        FreezeDefenderPosition(rightPosController);
        FreezeDefenderPosition(leftPosController);
    }

    private void ShiftToLeft()
    {
        shiftingToLeft = true;
        action = true;
        transform.localScale = new Vector3(-1f, 1f, 1f);
        playerAnim.SetTrigger("shift");
        FreezeDefenderPosition(leftPosController);
        FreezeDefenderPosition(rightPosController);
    }

    private bool IsAimOnGoal()
    {
        if (goal.activeInHierarchy)
        {
            if (goal.transform.position.y - 6 > aim.y) 
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        else
        {
            return false;
        }
    }

    private void AimCalculator()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 1;
        aim = mousePos;
    }

    private void Shoot()
    {
        // transform.localScale = new Vector3(1f, 1f, 1f);
        playerAnim.SetTrigger("shoot");
        playerAnim.ResetTrigger("dribble");
    }

    private void Pass()
    {
        // transform.localScale = new Vector3(1f, 1f, 1f);
        playerAnim.SetTrigger("self_pass");
        playerAnim.ResetTrigger("dribble");
    }

    private bool IsDefOnLeft()
    {
        Vector3 defVector = collideDef.transform.position - transform.position;
        Vector3 forwardVector = Vector3.forward;
        float angle = Vector3.SignedAngle(defVector, forwardVector, Vector3.up);

        return angle > 0;
    }

    private void Sprint()
    {
        spriteFlipper.FlipToRight();

        // transform.localScale = new Vector3(1f, 1f, 1f);

        playerAnim.SetTrigger("sprint");
        playerAnim.ResetTrigger("dribble");

        movementController.MoveToAim(ball.transform.position, shoulderContestOn ? attributeSettings.SholContestSpeed : attributeSettings.SprintSpeed);       
    }       

    private void Dribble()
    {
        transform.localScale = new Vector3(1f, 1f, 1f); // buraya koyarsan dodgelara da etki ediyor
        transform.Translate(dribbleSpeed * Time.deltaTime * Vector3.up);

        playerAnim.ResetTrigger("sprint");
        playerAnim.SetTrigger("dribble");
        transform.rotation = rotationToUp;
    }


    
    public void Shift()
    {
        if (HaveBall && !action)
        {
            action = true;

            #region Smart Shifting 

            // her iki savunmadan biri steal animasyonundaysa o tarafa kayılır ve dodge + stun olur.
            // aynı anda sağ ve sol pozisonlar doluysa seçim rastgele yapılır.
            // sol pozisyon boşsa sola kayılır.
            // sağ pozisyon boşsa sağa kayılır.
            // ikiside boşsa seçim rastgele yapılır.

            if (lDefenderController.IsStealing)
            {
                lDefenderController.Stun();
                ShiftToLeft();
            }
            else if (rDefenderController.IsStealing)
            {
                rDefenderController.Stun();
                ShiftToRight();
            }
            else if (rightPosController.IsPositionFull() && leftPosController.IsPositionFull())
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        ShiftToRight();
                        break;
                    case 2:
                        ShiftToLeft();
                        break;                    
                }
            }
            else if (!rightPosController.IsPositionFull() && !leftPosController.IsPositionFull())
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        ShiftToRight();
                        break;
                    case 2:
                        ShiftToLeft();
                        break;
                }
            }
            else if (rightPosController.IsPositionFull())
            {
                ShiftToLeft();
            }
            else if (leftPosController.IsPositionFull())
            {
                ShiftToRight();
            }
            #endregion
        }        
    }
   
    
    public void Fake()
    {
        if (HaveBall && !action) // IsStealing true iken stun yiyebilir burda da.
        {
            #region Smart Faking 

            if (rightPosController.IsPositionFull() && leftPosController.IsPositionFull())
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        FakeRight();
                        rDefenderController.Shake();
                        break;
                    case 2:
                        FakeLeft();
                        lDefenderController.Shake();
                        break;
                }
            }
            else if (!rightPosController.IsPositionFull() && !leftPosController.IsPositionFull())
            {
                switch (Random.Range(1, 3))
                {
                    case 1:
                        FakeRight();
                        rDefenderController.Shake();
                        break;
                    case 2:
                        FakeLeft();
                        lDefenderController.Shake();
                        break;
                }
            }
            else if (rightPosController.IsPositionFull())
            {
                FakeRight();
                rDefenderController.Shake();
            }
            else if (leftPosController.IsPositionFull())
            {
                FakeLeft();
                lDefenderController.Shake();
            }
            #endregion
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



