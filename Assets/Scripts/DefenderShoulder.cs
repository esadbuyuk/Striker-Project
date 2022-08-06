using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenderShoulder : MonoBehaviour
{   
    private Animator defenderAnim;    
    private Vector3 shContestloserPosition;
    private DestroyOutOfBounds destroyOutOfBounds;
    [SerializeField] private GameObject dangerText;
    private GameObject striker;
    private bool fallLeft;

    public bool Falled { get; private set; }

    void OnDisable()
    {
        if (destroyOutOfBounds.ResetDefender)
        {
            Falled = false;
        }
    }


    void Awake()
    {             
        destroyOutOfBounds = gameObject.GetComponent<DestroyOutOfBounds>();
        defenderAnim = gameObject.GetComponent<Animator>();
        striker = GameObject.Find("egoist");    
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    } 
    
    public void StartShoulderContest(bool defOnLeft)
    {
        gameObject.SetActive(false);
        fallLeft = defOnLeft;
        
        if (defOnLeft)
        {
            defenderAnim.SetFloat("Fall_to_left", 1f);
        }
        else
        {
            defenderAnim.SetFloat("Fall_to_left", 0f);
        }       
    }

    public void EndShoulderContest()
    {
        if (fallLeft)
        {
            shContestloserPosition.x = striker.transform.position.x - 0.8f; shContestloserPosition.y = striker.transform.position.y - 0.2f; shContestloserPosition.z = 6;
        }
        else
        {
            shContestloserPosition.x = striker.transform.position.x + 0.8f; shContestloserPosition.y = striker.transform.position.y - 0.2f; shContestloserPosition.z = 6;
        }

        transform.position = shContestloserPosition;
        Falled = true;
        gameObject.SetActive(true);
        defenderAnim.SetTrigger("Fall_to_right");
        dangerText.SetActive(false);
    }
}
