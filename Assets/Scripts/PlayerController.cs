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
    private Animator playerAnim;
    private Quaternion rotationToUp;
    private DefenderShoulder defenderShoulder;
    // [SerializeField] private GameObject rightDef;
    // [SerializeField] private GameObject leftDef;
    // private LDefenderController lDefenderController;
    // private RDefenderController rDefenderController;
    // public bool leftDfalling = false;
    // public bool rightDfalling = false;  
    private BallController ballController;
    private GameObject collideDef;
    private MovementController movementController;
    private SpriteFlipper spriteFlipper;
    private Vector3 aim;
    private bool shoulderContestOn = false;


    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        ballController = ball.GetComponent<BallController>();
        movementController = new MovementController(transform, attributeSettings);
        spriteFlipper = new SpriteFlipper(transform);       
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

        if (HaveBall)
        {
            EgoistDribble();
        }
        else
        {
            EgoistSprint();
        }
    }


    private void EgoistInputs()
    {/*
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

        if (Input.GetKeyDown(KeyCode.Q))
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


        if (Input.GetMouseButtonDown(0) && HaveBall && !IsPointerOverUIObject() && Time.timeScale == 1)
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


    private void OnTriggerEnter2D(Collider2D col) // Avoid, HaveBall
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

    public void PassBall() // ı call this with animation event.
    {
        ballController.Passed(aim);
        HaveBall = false;
    }

    public void ShootBall() // ı call this with animation event.
    {
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


    private void EgoistSprint()
    {
        spriteFlipper.FlipToRight();

        // transform.localScale = new Vector3(1f, 1f, 1f);

        playerAnim.SetTrigger("sprint");
        playerAnim.ResetTrigger("dribble");

        movementController.MoveToAim(ball.transform.position, shoulderContestOn ? attributeSettings.SholContestSpeed : attributeSettings.SprintSpeed);       
    }
       

    private void EgoistDribble()
    {
        // transform.localScale = new Vector3(1f, 1f, 1f); buraya koyarsan dodgelara da etki ediyor

        playerAnim.ResetTrigger("sprint");
        playerAnim.SetTrigger("dribble"); // animation has a movement in itself.
        transform.rotation = rotationToUp;
    }

    

    /*
    public void GoRight()
    {
        if (haveBall)
        {           
            if (RDefenderController.getPositionedR) // useless
            {
                // rDefenderController.Steal();
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); // flip the animation to right
                playerAnim.SetTrigger("dodge"); // Go_right olarak de�i�tirilip blend tree yap�l�cak.

            }

            if (LDefenderController.getPositionedL)// useless
            {

                // StartCoroutine(lDefenderController.StunCountdownRoutine(1));
            }

            if (LDefenderController.getPositionedL) // useless
            {
                // lDefenderController.Steal();
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                playerAnim.SetTrigger("dodge");
            }

            if (RDefenderController.getPositionedR) // useless
            {

                // StartCoroutine(rDefenderController.StunCountdownRoutine(1));
            }
        }

        if (shoot)
        {
            moveForward.spinToRight = true;
        }
        
    }

    /*
    public void GoLeft()
    {
        if (haveBall)
        {                 
            if (LDefenderContoller.getPositionedL) // useless
            {
                // lDefenderController.Steal();
            }
            else
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                playerAnim.SetTrigger("dodge");
            }

            if (RDefenderController.getPositionedR) // useless
            {
                
                // StartCoroutine(rDefenderController.StunCountdownRoutine(1));
            }
        }           

    }*/
    /*
    public void FakeRight()
    {
        if (haveBall && RDefenderController.getPositionedR)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            playerAnim.SetTrigger("fake"); // Fake_right olarak de�i�tirilip blend tree yap�l�cak.
            rDefenderController.Steal();                              
        }

        if (shoot)
        {
            moveForward.spinToRight = true;
        }

        if (haveBall && LDefenderController.getPositionedL)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            playerAnim.SetTrigger("fake"); // Fake_left olarak de�i�tirilip blend tree yap�l�cak.                        
            lDefenderController.Steal();
        }
    }*/

    /*
    public void FakeLeft()
    {
        if (haveBall && LDefenderContoller.getPositionedL)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            playerAnim.SetTrigger("fake"); // Fake_left olarak de�i�tirilip blend tree yap�l�cak.                        
            lDefenderController.Steal();            
        }
               

    }

    */

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



