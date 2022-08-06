using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Animator playerAnim;
    //public GameObject projectilePrefab;
    // public bool haveBall = true;
    public static bool haveBall = true;
    public GameObject ball;
    public static float sprintSpeed = 8.0f;
    public static float shContestSpeed = 3.2f;
    private Quaternion rotationToUp;
    private DefenderShoulder defenderShoulder;
    public GameObject rightDef;
    public GameObject leftDef;
    private LDefenderController lDefenderController;
    private RDefenderController rDefenderController;
    private Vector3 shContestloserPosition;
    private bool shoulderContestOn = false;
    private Animator defAnim;
    public bool leftDfalling = false;
    public bool rightDfalling = false;
    private Vector3 forwardVector;
    private Vector3 defVector;
    private float angle;
    // private bool defOnLeft;
    private MoveForward moveForward;
    public static float goLeftNo = 1;
    public GameObject goal;
    public Vector3 aim;
    private Vector3 mousePos;
    public static bool shoot = false;
    public Button fakeButton;
    private GameObject collideDef;

    void Start()
    {
        haveBall = true;
        playerAnim = GetComponent<Animator>();

        // ball = GameObject.Find("ball");
        moveForward = ball.GetComponent<MoveForward>();

        rotationToUp = transform.rotation;

        rDefenderController = rightDef.GetComponent<RDefenderController>();
        lDefenderController = leftDef.GetComponent<LDefenderController>(); // bunları public ile alman gerek cunku kompozisyon yaptın.

        playerAnim.SetFloat("dodge_no", goLeftNo);// bunlar� update �a��r�yosan pek elveri�li de�il!


        // moveForward = GameObject.Find("empty ball").GetComponent<MoveForward>();        
    }


    void Update()
    {
        
    }


    private void LateUpdate() // update yap�lcak!
    {
        EgoistInputs();

        if (haveBall)
        {
            EgoistDribble();
        }
        else
        {
            EgoistSprint();
        }
    }
    
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("defender_body")) // defenderlar�n isTrigger a��k.
        {
            if (!haveBall && !shoulderContestOn)
            {
                // animation event works here!

                // rightDef = GameObject.Find("defender right");
                // leftDef = GameObject.Find("defender left");

                collideDef = col.gameObject;
                defenderShoulder = collideDef.GetComponent<DefenderShoulder>();        

                if (IsDefOnLeft())
                {
                    // defender soldaysa                    
                    playerAnim.SetFloat("def_onleft", 1f);                   
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
            

            /*else if (haveBall)
            {
                playerAnim.SetTrigger("Avoid"); // bunu defender�n tackle yapt��� ve freeball an�nda aktifle�tirmek daha mant�kl� farkl� bir child collider gerek // egoistten rb ��kar�nca do�ru �al��t�.
                Debug.Log("Avoid");
            }  // avoid d�zg�n �al��m�yor o y�zden iptal ettim.*/
        }               
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("tackle collider"))
        {
            if (!haveBall && !shoulderContestOn)
            {
                playerAnim.SetTrigger("avoid");
                Debug.Log("avoid çağrıldı");
            }
            
        }
        
        if (col.gameObject.CompareTag("ball"))
        {
            haveBall = true;
            playerAnim.ResetTrigger("sprint");
            playerAnim.SetTrigger("dribble");
            
            if (shoulderContestOn == true)
            {
                /*
                if (defenderShoulder == leftDef) // delegasyon yapılır mı diye bir bak.
                {
                    if (IsDefOnLeft())
                    {
                        // defender soldaysa
                        // Debug.Log("fall anim on");
                        // defAnim.SetFloat("Fall_to_left", 1f);
                        // leftDfalling = true;

                        shContestloserPosition.x = transform.position.x - 0.8f;
                        shContestloserPosition.y = transform.position.y - 0.2f;
                        shContestloserPosition.z = 6;
                    }
                    else
                    {
                        // defender sa�daysa Debug.Log("fall anim on");
                        // defAnim.SetFloat("Fall_to_left", 0f);
                        // leftDfalling = true;

                        shContestloserPosition.x = transform.position.x + 0.8f;
                        shContestloserPosition.y = transform.position.y - 0.2f;
                        shContestloserPosition.z = 6;
                    }
                }

                if (defenderShoulder == rightDef)
                {                    
                    if (IsDefOnLeft())
                    {
                        // defender soldaysa
                        // Debug.Log("fall anim on");
                        // transform.localScale = new Vector3(-1f, 1f, 1f); bunu kullan animasyonlar� mirrorlamak i�in
                        // defAnim.SetFloat("Fall_to_left", 1f);
                        // rightDfalling = true;

                        shContestloserPosition.x = transform.position.x - 0.8f;
                        shContestloserPosition.y = transform.position.y - 0.2f;
                        shContestloserPosition.z = 6;
                    }
                    else
                    {
                        // defender sa�daysa
                        // Debug.Log("fall anim on");
                        // defAnim.SetFloat("Fall_to_left", 0f);
                        // rightDfalling = true;

                        shContestloserPosition.x = transform.position.x + 0.8f;
                        shContestloserPosition.y = transform.position.y - 0.2f;
                        shContestloserPosition.z = 6;
                    }
                }
                */
                
                
                playerAnim.SetBool("shoulder_contest", false);
                shoulderContestOn = false;
                defenderShoulder.EndShoulderContest();
            }
        }
    }

    public void SetDefInActive() // delegasyon yapılır mı diye bir bak.
    {
        if (shoulderContestOn == true)
        {
            defenderShoulder.StartShoulderContest(IsDefOnLeft());
        }
    }


    private void EgoistInputs()
    {
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

                if (shoot) // bunları buraya koymaya gerek yok!
                {
                    moveForward.spinToRight = true;
                }
            }
            else if (rDefenderController.stunned && !LDefenderController.getPositionedL) // sa� savunma stunlanm�� ve sol savunma pozisyon almam�� ise sola kayma yap�labilsin.
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                playerAnim.SetTrigger("dodge");

                if (shoot)
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

                if (shoot)
                {
                    moveForward.spinToRight = true;
                }
            }
            else if (!LDefenderController.getPositionedL) 
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                playerAnim.SetTrigger("dodge");

                if (shoot)
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

                if (shoot)
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

                if (shoot)
                {
                    moveForward.spinToLeft = true;

                }
            }
        }

        if (Input.GetMouseButtonDown(0) && haveBall && !IsPointerOverUIObject() && Time.timeScale == 1)
        {
            AimCalculator();

            if (goal.activeInHierarchy)
            {
                if (goal.transform.position.y - 6 > aim.y)
                {
                    shoot = false;

                    transform.localScale = new Vector3(1f, 1f, 1f);
                    playerAnim.SetTrigger("self_pass");
                    playerAnim.ResetTrigger("dribble");

                }
                else if (goal.transform.position.y - 6 <= aim.y)
                {                   
                    shoot = true;

                    transform.localScale = new Vector3(1f, 1f, 1f);
                    playerAnim.SetTrigger("shoot");
                    playerAnim.ResetTrigger("dribble");

                }
           
            }
            else
            {
                shoot = false;

                transform.localScale = new Vector3(1f, 1f, 1f);
                playerAnim.SetTrigger("self_pass");
                playerAnim.ResetTrigger("dribble");

            }

        }

    }


    private void EgoistSprint()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);

        playerAnim.SetTrigger("sprint");
        playerAnim.ResetTrigger("dribble");

        Vector3 lookdirection = ball.transform.position - transform.position; //normalized gelebilir.                           
        Quaternion rotationToBall = Quaternion.LookRotation(Vector3.forward, lookdirection);

        transform.rotation = rotationToBall;
        

        if (shoulderContestOn)
        {
            transform.Translate(Vector3.up * Time.deltaTime * shContestSpeed);
        }
        else
        {
            transform.Translate(Vector3.up * Time.deltaTime * sprintSpeed);
        }
    }


    private void EgoistDribble()
    {
        // transform.localScale = new Vector3(1f, 1f, 1f); buraya koyarsan dodgelara da etki ediyor
                
        playerAnim.SetTrigger("dribble");
        transform.rotation = rotationToUp;
    }


    private bool IsDefOnLeft()
    {
        defVector = collideDef.transform.position - transform.position;
        forwardVector = Vector3.forward;       
        angle = Vector3.SignedAngle(defVector, forwardVector, Vector3.up);

        /* print(angle);
        // def soldaysa pozitif a��

        if (angle > 0)
        {
            return true;
        }
        else
        {
            return false;
        }*/

        return angle > 0; // burayı kontrol etmedim oyunda sorun varsa geri düzelt.
    }


    private void HavenotBall()
    {
        haveBall = false;
        moveForward.SendBall();
    }


    private void AimCalculator()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 1;
        aim = mousePos;

    }


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
    }

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



