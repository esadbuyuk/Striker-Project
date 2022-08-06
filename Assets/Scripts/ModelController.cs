using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    private Animator modelAnim;
    private Quaternion firstRot;
    private Vector3 firstPos;


    // Start is called before the first frame update
    void Start()
    {
        firstPos = new Vector3(0, -4.5f, transform.position.z); ;
        firstRot = transform.rotation;
        modelAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
         // transform.position = new Vector3(transform.position.x, -4.5f, transform.position.z);
        // transform.position = new Vector3(transform.position.x, transform.position.y, 10);
    }

    public void GoRight()
    {
        transform.position = firstPos;
        transform.rotation = firstRot;
        modelAnim.SetTrigger("Go_right"); // Go_right olarak de�i�tirilip blend tree yap�l�cak.
        // modelAnim.ResetTrigger("Go_right");
    }

    public void GoLeft()
    {
        transform.position = firstPos;
        transform.rotation = firstRot;
        modelAnim.SetFloat("Goleft_no", PlayerController.goLeftNo);
        modelAnim.SetTrigger("Go_left");
        // modelAnim.ResetTrigger("Go_left");
    }

    public void FakeRight()
    {
        transform.position = firstPos;
        transform.rotation = firstRot;
        modelAnim.SetTrigger("Inside_Stepover_R"); // Fake_right olarak de�i�tirilip blend tree yap�l�cak.
        // modelAnim.ResetTrigger("Inside_Stepover_R");
    }

    public void FakeLeft()
    {
        transform.position = firstPos;
        transform.rotation = firstRot;
        modelAnim.SetTrigger("Inside_Stepover_L"); // Fake_left olarak de�i�tirilip blend tree yap�l�cak.
        // modelAnim.ResetTrigger("Inside_Stepover_L");
    }

    public void GoFirstPos()
    {
        Debug.Log("Animation event called");
        transform.SetPositionAndRotation(firstPos, firstRot);
    }
}
