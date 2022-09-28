using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dangerText;
    private Vector3 distanceToBall;
    private Animator defenderAnim;
    private PlayerController playerController;
    private float tackleSpeed = 5.0f;
    // private bool ignoreCountdown;
    [SerializeField] private GameObject ball;
    private Vector3 ballPosition;
    private MovementController movementController;


    void Awake()
    {
        defenderAnim = gameObject.GetComponent<Animator>();
        playerController = GameObject.Find("egoist").GetComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetMovementController(MovementController mc)
    {
        movementController = mc;
    }    


    public void ActivateSliderBehaviour()
    {
        DistanceToBallCalculator();

        dangerText.gameObject.SetActive(true);

        if (distanceToBall.y < 3.5f)
        {
            defenderAnim.SetTrigger("Tackle");
            Tackle();
        }
        else if (!playerController.HaveBall) // top oyuncuda de�ilken
        {
            defenderAnim.ResetTrigger("Runback");
            defenderAnim.SetTrigger("Sprint");
            RunToBall();
        }
        else
        {
            defenderAnim.ResetTrigger("Runback");
            defenderAnim.SetTrigger("Sprint");
            RunForTackle();
        }
    }


    private Vector3 DistanceToBallCalculator()
    {
        distanceToBall = transform.position - ball.transform.position;
        return distanceToBall;
    }


    private void Tackle()
    {
        // getPositioned = false;

        transform.Translate(tackleSpeed * Time.deltaTime * Vector3.up);
        // ignoreCountdown = true;
    }


    private void RunToBall()
    {
        movementController.MoveToAim(ball.transform.position); // burda sprintSpeed parametresi olmasa da olur.
        /*
        Vector3 lookdirection = ball.transform.position - transform.position; // normalized gelebilir.                           
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookdirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);*/
    }


    private void RunForTackle()
    {
        ballPosition.z = transform.position.z;
        ballPosition.x = ball.transform.position.x + 0.0f;
        ballPosition.y = ball.transform.position.y + 1.2f;

        movementController.MoveToAim(ballPosition); // burda sprintSpeed parametresi olmasa da olur.

        /*
        Vector3 lookDirection = ballPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);*/
    }
}