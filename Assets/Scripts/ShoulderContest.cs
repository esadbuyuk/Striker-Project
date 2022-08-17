using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderContest : MonoBehaviour
{
    /*
    private bool shoulderContestOn = false;
    public Animator defenderAnim;
    private GameObject egoist;
    private Animator egoistAnim;
    private TimerBehaviour timerBehaviour;
    private Vector3 shContestloserPosition;
    private DestroyOutOfBounds destroyOutOfBounds;
    

    void OnDisable()
    {
        if (destroyOutOfBounds.ResetDefender)
        {
            timerBehaviour.defenderIsFalling = false;            
        }
    }


    void Awake()
    {
        egoist = GameObject.Find("egoist");
        egoistAnim = egoist.GetComponent<Animator>();

        timerBehaviour = gameObject.GetComponent<TimerBehaviour>();

        destroyOutOfBounds = gameObject.GetComponent<DestroyOutOfBounds>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("egoist")) // defenderlarin isTrigger acik.
        {
            if (!PlayerController.haveBall && !shoulderContestOn)
            {
                // rightDef = GameObject.Find("defender right");
                // leftDef = GameObject.Find("defender left");
                // collideDef = col.gameObject;
                // defenderAnim = collideDef.GetComponent<Animator>();

                if (IsDefOnLeft())
                {
                    // defender soldaysa                    
                    egoistAnim.SetFloat("def_onleft", 1f);
                }
                else
                {
                    // defender sagdaysa
                    egoistAnim.SetFloat("def_onleft", 0f);
                }

                Debug.LogWarning("ShoulderContest animasyonuna girdi");
                egoistAnim.ResetTrigger("sprint");
                egoistAnim.SetBool("shoulder_contest", true);
                gameObject.SetActive(false);
                shoulderContestOn = true;                
            }

            /*else if (haveBall)
            {
                playerAnim.SetTrigger("Avoid"); // bunu defenderin tackle yapt��� ve freeball aninda aktiflestirmek daha mantikli farkli bir child collider gerek // egoistten rb cikarinca dogru calisti.
                Debug.Log("Avoid");
            }  // avoid duzgun calismiyor o yuzden iptal ettim.
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {        
        if (col.gameObject.CompareTag("ball"))
        {
            if (shoulderContestOn)
            {
                // timerBehaviour.defenderIsFalling = true;                

                if (IsDefOnLeft())
                {
                    // defender soldaysa
                    // Debug.Log("fall anim on");
                    defenderAnim.SetFloat("Fall_to_left", 1f);

                    shContestloserPosition.x = egoist.transform.position.x - 0.8f;
                    shContestloserPosition.y = egoist.transform.position.y - 0.2f;
                    shContestloserPosition.z = 6;
                }
                else
                {
                    // defender sa�daysa Debug.Log("fall anim on");
                    defenderAnim.SetFloat("Fall_to_left", 0f);

                    shContestloserPosition.x = egoist.transform.position.x + 0.8f;
                    shContestloserPosition.y = egoist.transform.position.y - 0.2f;
                    shContestloserPosition.z = 6;
                }
               

                transform.position = shContestloserPosition;
                // defAnim.SetTrigger("Fall_to_right"); // bunu timerBehaviour da çağırdın.

                egoistAnim.SetTrigger("dribble");
                egoistAnim.SetBool("shoulder_contest", false);
                gameObject.SetActive(true);
                shoulderContestOn = false;                
            }
            else
            {
                PlayerController.haveBall = true;
                egoistAnim.ResetTrigger("sprint");
                egoistAnim.SetTrigger("dribble");
            }
        }
    }

    public void SetDefInActive() // delegasyon yapılır mı diye bir bak.
    {
        if (shoulderContestOn == true)
        {
            gameObject.SetActive(false);
        }
    }


    private bool IsDefOnLeft()
    {
        Vector3 defVector = transform.position - egoist.transform.position;
        Vector3 forwardVector = Vector3.forward;
        float angle = Vector3.SignedAngle(defVector, forwardVector, Vector3.up);

         print(angle + ", defender soldaysa acı pozitif oluyor.");

        /*

        if (angle > 0)
        {
            return true;
        }
        else
        {
            return false;
        }

        // def soldaysa pozitif a��
        return angle > 0; // burayı kontrol etmedim oyunda sorun varsa geri düzelt.
    }
*/
}
