using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GoalKeeperController : MonoBehaviour
{
    private GameObject egoist;
    private GameObject ball;
    private GameObject goalObj;
    private Animator goalkeeperAnim;
    private bool aimOnLeft;
    private Vector3 forwardVector;
    private Vector3 aimVector;
    private float angle;
    private bool uncontrollable = false;
    [SerializeField] private AttributeSettings attributeSettings;
    private MovementController movementController;
    private bool tackle;

    void OnEnable()
    {      
        goalkeeperAnim.Rebind();
        goalkeeperAnim.Update(0f);

        uncontrollable = false;
        transform.position = new Vector3(goalObj.transform.position.x, goalObj.transform.position.y - 3.6f, 11);
        transform.eulerAngles = new Vector3(0, 0, 180);
    }

    void Awake()
    {
        goalObj = GameObject.Find("goal");
        ball = GameObject.Find("ball");
        egoist = GameObject.Find("egoist");
        goalkeeperAnim = GetComponent<Animator>();
        movementController = new MovementController(transform, attributeSettings);
    }

    // Start is called before the first frame update
    void Start()
    {       
        transform.position = new Vector3(goalObj.transform.position.x, goalObj.transform.position.y - 3.6f, 11);
    }

    void LateUpdate()
    {
        if (uncontrollable)
        {

        }
        else if ((transform.position.y - ball.transform.position.y < 3) )
        {
            Tackle();
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

    public void JumpToBall(Vector3 aim)
    {
        if (tackle != true)
        {
            uncontrollable = true;

            if (IsAimOnLeft(aim))
            {
                goalkeeperAnim.SetTrigger("jump_to_left");
            }
            else
            {
                goalkeeperAnim.SetTrigger("jump_to_right");
            }
        }        
    }


    private void RunToBall()
    {
        goalkeeperAnim.SetTrigger("sprint");

        movementController.MoveToAim(ball.transform.position);
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
        transform.Translate(attributeSettings.SprintSpeed * Time.deltaTime * Vector3.up);
        goalkeeperAnim.SetTrigger("tackle");
        tackle = true;
    }


    private bool IsAimOnLeft(Vector3 aim)
    {
        aimVector = aim - transform.position;
        forwardVector = Vector3.forward;
        angle = Vector3.SignedAngle(aimVector, forwardVector, Vector3.up);

        print(angle);
        // generally, angle is positive if varible is on left.
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
}
