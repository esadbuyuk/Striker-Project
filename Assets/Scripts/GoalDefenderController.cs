using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class GoalDefenderController : MonoBehaviour
{
   
    private Animator leftDefenderAnim;
    public static bool getPositionedG = false;     
    private GameObject ball;
    private float sprintSpeed = 5.0f;
    private GameObject egoist;
    public Vector3 positionOffset;    
    private Vector3 ballPosition;   
    private int defenderType;
    private bool slider = false;   
    private Vector3 distanceToEgoist; // bunlara gerek yokmuş aslında.
    private Vector3 distanceToBall;
    private Vector3 lookDirection;    
    private PlayerController playerController;    
    private DestroyOutOfBounds destroyOutOfBounds;
    private SpriteRenderer mySpriteRenderer;

    void OnDisable()
    {
        if (destroyOutOfBounds.resetLeft)
        {
            leftDefenderAnim.SetTrigger("Sprint");

            playerController.leftDfalling = false;      
            
            slider = false;
            
            

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

        playerController = egoist.GetComponent<PlayerController>();
        destroyOutOfBounds = gameObject.GetComponent<DestroyOutOfBounds>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Start()
    {        
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
        
    }


    private void DefenderTypeChooser()
    {
        defenderType = 1;

        if (defenderType == 1) // 0 1 yap�lcak
        {
            slider = true;
        }
       
    }


    private void SliderBehaviour()
    {
        DistanceToBallCalculator();

        if (distanceToBall.y < 4)
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


   

    private void RunForTackle()
    {        
       

        ballPosition.z = transform.position.z;
        ballPosition.x = ball.transform.position.x + 0.0f;
        ballPosition.y = ball.transform.position.y + 0.3f;
        lookDirection = ballPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
    }


    

    private void RunToBall()
    {
       

        Vector3 lookdirection = ball.transform.position - transform.position;//normalized gelebilir.                           
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookdirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
    }


    private void Tackle()
    {
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
      
    }
          

    public void StealFinished() // bu private d� denemek i�in public yapt�n!
    {
       
       
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

    private void ErrorDetector()
    {
        Debug.Log("animation called!");
    }
}
