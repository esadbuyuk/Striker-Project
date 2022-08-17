using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerDesign : MonoBehaviour
{
    private Animator playerAnim;
    [SerializeField] private AttributeSettings attributeSettings;
    [SerializeField] private SkillSettings skillSettings;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = GetComponent<Animator>();
        playerAnim.SetFloat("dodge_no", skillSettings.DodgeNo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeDodgeSkill()
    {
        skillSettings.NextDodge();
    }

    public void FastStrikerSelected()
    {
        // fastStrikerData yı playerController ın attribute Settings değişkenine atıycaksın. Data bilgileri aşağıda.

        // PlayerController.sprintSpeed = 8;
        // PlayerController.shContestSpeed = 3.2f;
        // ballController.shootSpeed = 17;
    }

    public void StrongStrikerSelected()
    {
        // StronhStrikerData yı playerController ın attribute Settings değişkenine atıycaksın. Data bilgileri aşağıda.

        // PlayerController.sprintSpeed = 5.5f;
        // PlayerController.shContestSpeed = 4.2f;
        // ballController.shootSpeed = 27;
    }
}
