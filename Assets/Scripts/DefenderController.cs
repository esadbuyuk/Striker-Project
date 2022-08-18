using TMPro;
using UnityEngine;

public class DefenderController : MonoBehaviour
{
    private DefenderTypeChooser defenderTypeChooser;
    private SliderBehaviour sliderBehaviour;
    private Animator defenderAnim;
    private DefenderShoulder defenderShoulder;
    private DestroyOutOfBounds destroyOutOfBounds;
    [SerializeField] private TextMeshProUGUI dangerText;
    private SpriteRenderer mySpriteRenderer;
    private ITimerBehaviour timerBehaviour;



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
        timerBehaviour = GetComponent<ITimerBehaviour>();
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

    void LateUpdate()
    {
        
    }
}
