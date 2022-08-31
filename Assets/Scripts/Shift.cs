using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shift : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleDeathAnimations()
    {
        int i = Random.Range(1, 4); // range is X to Y -1 for ints. [1-3 here]
        switch (i)
        {
            case 1:
                animator.SetTrigger("Death1");
                break;
            case 2:
                animator.SetTrigger("Death2");
                break;
            case 3:
                animator.SetTrigger("Death3");
                break;
        }
    }
}
