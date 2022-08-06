using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DefenderController : MonoBehaviour
{
    private DefenderTypeChooser defenderTypeChooser;
    private SliderBehaviour sliderBehaviour;
    private TimerBehaviour timerBehaviour;
    private Animator defenderAnim;
    private DefenderShoulder defenderShoulder;
    private DestroyOutOfBounds destroyOutOfBounds;
    public TextMeshProUGUI dangerText;
    private SpriteRenderer mySpriteRenderer;



    void OnDisable()
    {
        if (destroyOutOfBounds.ResetDefender)
        {
            defenderAnim.SetTrigger("Sprint");            
            dangerText.gameObject.SetActive(false);

            defenderTypeChooser.ReselectDefenderType();

            if (mySpriteRenderer != null)
            {
                // flip the sprite
                mySpriteRenderer.flipX = false;
                mySpriteRenderer.flipY = false;
            }
        }
    }


    void Awake()
    {
        defenderAnim = GetComponent<Animator>();
        defenderShoulder = GetComponent<DefenderShoulder>();
        destroyOutOfBounds = GetComponent<DestroyOutOfBounds>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        defenderTypeChooser = GetComponent<DefenderTypeChooser>();
        sliderBehaviour = GetComponent<SliderBehaviour>();
        timerBehaviour = GetComponent<TimerBehaviour>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (mySpriteRenderer != null)
        {
            // flip the sprite
            mySpriteRenderer.flipX = false;
            mySpriteRenderer.flipY = false;
        }                
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LateUpdate()
    {
        if (defenderShoulder.Falled)
        {
            // bu koşulda aşağıdakiler geçersiz kalsın.
            transform.position = transform.position;
        }
        else if (defenderTypeChooser.slider)
        {
            sliderBehaviour.ActivateSliderBehaviour();
        }
        else if (defenderTypeChooser.timer)
        {
            timerBehaviour.ActivateTimerBehaviour();
        }
    }
}
