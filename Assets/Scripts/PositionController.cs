using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour
{
    public GameObject egoist;
    public Vector3 offset;
    private bool freeze = false;
    private bool shiftToRight;
    private bool shiftToLeft;
    private bool isPositionFull;
    private Animator animator;
    [SerializeField] Vector3 stealRotation;
    private Vector3 firstRotation;
    private Vector3 firstPosition;
    private bool rotateForSteal = false;
    private bool shakeToLeft;
    private bool shakeToRight;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        firstRotation = transform.rotation.eulerAngles;
        firstPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (rotateForSteal)
        {
            transform.eulerAngles = stealRotation;
        }
        else
        {
            transform.eulerAngles = firstRotation;
        }
       
        if (!freeze)
        {
            transform.position = egoist.transform.position + offset;
        }
        else // that is for stay on vertical offset but not in lateral
        {
            transform.position = new Vector3(transform.position.x, egoist.transform.position.y + offset.y, transform.position.z);
        }


        if (rotateForSteal)
        {

        }
        else if (shiftToRight)
        {
            transform.position += new Vector3(0.01f, 0, 0);
            // transform.Translate(0.02f, 0, 0); bu arkadaş pozisyonu local olarak ölçüyor.
            if (transform.position.x > egoist.transform.position.x + offset.x)
            {
                shiftToRight = false;
                freeze = false;
            }            
        }
        else if (shiftToLeft)
        {
            transform.position += new Vector3(-0.01f, 0, 0);
            // transform.Translate(-0.02f, 0, 0); bu arkadaş pozisyonu local olarak ölçüyor.
            if (transform.position.x < egoist.transform.position.x + offset.x)
            {
                shiftToLeft = false;
                freeze = false;
            }
        }

        /*if (rotateForSteal)
        {
            // this is for being unable to dodge stealing defender with fake skills.
        }
        else*/ if (shakeToRight)
        {
            transform.position += new Vector3(0.01f, 0, 0);
            if (transform.position.x > egoist.transform.position.x + offset.x + 1)
            {
                shakeToRight = false;
                shiftToLeft = true;
                freeze = true;

            }
        }
        else if (shakeToLeft)
        {           
            transform.position += new Vector3(-0.01f, 0, 0);
            if (transform.position.x < egoist.transform.position.x + offset.x - 1)
            {
                shakeToLeft = false;
                shiftToRight = true;
                freeze = true;

            }
        }
    }

    public void Shake()
    {
        Freeze();

        if (transform.position.x > egoist.transform.position.x)
        {
            shakeToRight = true;
        }
        else if (transform.position.x < egoist.transform.position.x)
        {
            shakeToLeft = true;
        }
    }

    public void MoveDown()
    {
        animator.SetTrigger("move_down");
    }

    public void Freeze()
    {
        freeze = true;      
    }


    public void Shift()
    {
        Debug.Log("position -> Shift()");
        if (transform.position.x > egoist.transform.position.x + offset.x)
        {
            shiftToLeft = true;

        }
        else if (transform.position.x < egoist.transform.position.x + offset.x)
        {
            shiftToRight = true;

        }
    }

    public void ResetCoordinates()
    {
        transform.position = firstPosition;
        ResetRotation();
    }

    public void PositionIsFilled()
    {
        isPositionFull = true;
    }

    public void PositionIsEmptied()
    {
        isPositionFull = false;
    }

    public bool IsPositionFull()
    {
        return isPositionFull;
    }

    public void StealMovement()
    {
        rotateForSteal = true;
        animator.SetTrigger("steal_movement");
    }

    private void ActionCompleted()
    {
        ResetRotation();        
    }

    public void ResetRotation()
    {
        rotateForSteal = false;
    }
}
