using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoalKeeperController : MonoBehaviour
{
    private GameObject egoist;
    private GameObject ball;
    private GameObject goalObj;
    private PlayerController playerController; // static variable kulland���n i�in buna gerek kalmad�
    private MoveForward moveForward;
    private float sprintSpeed = 4.0f;
    private Animator goalkeeperAnim;
    private bool aimOnLeft;
    private Vector3 forwardVector;
    private Vector3 aimVector;
    private float angle;
    private bool shoot = false;
    // private Quaternion firstRot;
    // private bool gShoot;



    void OnEnable()
    {      
        goalkeeperAnim.Rebind();
        goalkeeperAnim.Update(0f);

        shoot = false;
        transform.position = new Vector3(goalObj.transform.position.x, goalObj.transform.position.y - 3.6f, 11);
        transform.eulerAngles = new Vector3(0, 0, 180);
    }

    void Awake()
    {
        goalObj = GameObject.Find("goal");
        ball = GameObject.Find("ball");
        egoist = GameObject.Find("egoist");
        // playerController = egoist.GetComponent<PlayerController>();
        goalkeeperAnim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {       
        transform.position = new Vector3(goalObj.transform.position.x, goalObj.transform.position.y - 3.6f, 11);
        // firstRot = new Vector2(0, 0, 180);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        moveForward = ball.GetComponent<MoveForward>();// bunu buraya koyma!

        // gShoot = moveForward.shoot;

        if (shoot)
        {

        }
        else if ((transform.position.y - ball.transform.position.y < 3) ) //|| (transform.position.y - ball.transform.position.y < 0))
        {
            Tackle();
        }               
        else if(Input.GetMouseButtonDown(0) && PlayerController.haveBall && PlayerController.shoot && !IsPointerOverUIObject() && Time.timeScale == 1)
        {
            shoot = true;
            // Debug.Log("goalkeeperdaki shoot:" + gShoot);

            if (IsAimOnLeft())
            {
                Debug.Log("solda");
                goalkeeperAnim.SetTrigger("jump_to_left");
            }
            else
            {
                Debug.Log("sa�da");
                goalkeeperAnim.SetTrigger("jump_to_right");
            }
        }
        else if (transform.position.y - ball.transform.position.y < 8)
        {
            RunToBall();
        }
        else
        {
            FollowEgoist();
        }
    }

    private void RunToBall()
    {
        goalkeeperAnim.SetTrigger("sprint");               

        Vector3 lookdirection = ball.transform.position - transform.position;//normalized gelebilir.                           
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookdirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
    }


    private void FollowEgoist()
    {
        if (transform.position.y - egoist.transform.position.y < 9)
        {
            if (transform.position.x - egoist.transform.position.x > 4)
            {
                transform.Translate(0.5f, 0, 0);
            }

            if (transform.position.x - egoist.transform.position.x < -4)
            {
                transform.Translate(-0.5f, 0, 0);
            }
        }
        
    }


    private void Tackle()
    {
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
        goalkeeperAnim.SetTrigger("tackle");
    }


    private bool IsAimOnLeft()
    {
        aimVector = moveForward.aim - transform.position;
        forwardVector = Vector3.forward;
        angle = Vector3.SignedAngle(aimVector, forwardVector, Vector3.up);

        print(angle);
        // def soldaysa pozitif a��

        if (angle > 0)
        {
            aimOnLeft = false;
        }
        else
        {
            aimOnLeft = true;
        }

        return aimOnLeft;
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
