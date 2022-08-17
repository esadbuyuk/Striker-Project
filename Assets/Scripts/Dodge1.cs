using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge1 : MonoBehaviour
{
    private ITimerBehaviour rightTimerBehaviour;
    private ITimerBehaviour leftTimerBehaviour;
    [SerializeField] private GameObject rightDef;
    [SerializeField] private GameObject leftDef;


    private void Awake()
    {
        rightTimerBehaviour = rightDef.GetComponent<ITimerBehaviour>();
        leftTimerBehaviour = leftDef.GetComponent<ITimerBehaviour>();
    }
    /* // defenderları düzeltmeden burayı yapamıyacağın için erteledin.
    private PlayerController playerController;


    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && playerController.HaveBall)
        {
            if (lDefenderController.stunned && !RDefenderController.getPositionedR) // sol savunma stunlanm�� ve sa� savunma pozisyon almam�� ise sa�a kayma yap�labilsin.
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                playerAnim.SetTrigger("dodge"); // Go_right olarak de�i�tirilip blend tree yap�l�cak.

                
            }
            else if (rDefenderController.stunned && !LDefenderController.getPositionedL) // sa� savunma stunlanm�� ve sol savunma pozisyon almam�� ise sola kayma yap�labilsin.
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                playerAnim.SetTrigger("dodge");

                
            }
            else if (!RDefenderController.getPositionedR) // sa� savunma pozisyon almam�� ise sa�a kayma yap�labilsin.
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
                playerAnim.SetTrigger("dodge"); // Go_right olarak de�i�tirilip blend tree yap�l�cak.

               
            }
            else if (!LDefenderController.getPositionedL)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                playerAnim.SetTrigger("dodge");

               
            }
        }
    }*/
}
