using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SliderBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dangerText;
    private Vector3 distanceToBall;
    private Animator defenderAnim;
    // private bool getPositioned = false; // yalnızca timer da getpositioned olmasını sağla.
    private float sprintSpeed = 5.0f; // bu değişkeni statik yapabilirsin ileride! cünkü her yerde farklı olabilir.
    // private bool ignoreCountdown;
    [SerializeField] private GameObject ball; // private idi public yaptın sorun yoksa silebilirsin.
    private Vector3 ballPosition;


    void Awake()
    {
        defenderAnim = gameObject.GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        else if (!PlayerController.haveBall) // top oyuncuda de�ilken
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

        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
        // ignoreCountdown = true;
    }


    private void RunToBall()
    {
        // getPositioned = false;

        // ignoreCountdown = true; slider da countdown ın başlamamasını sağla.

        Vector3 lookdirection = ball.transform.position - transform.position; // normalized gelebilir.                           
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookdirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
    }


    private void RunForTackle()
    {
        // getPositioned = false;
        // ignoreCountdown = true;
               
        ballPosition.z = transform.position.z;
        ballPosition.x = ball.transform.position.x + 0.0f;
        ballPosition.y = ball.transform.position.y + 1.2f;
        Vector3 lookDirection = ballPosition - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, lookDirection);

        transform.rotation = rotation;
        transform.Translate(sprintSpeed * Time.deltaTime * Vector3.up);
    }
}