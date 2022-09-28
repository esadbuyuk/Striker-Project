using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrikerDesign : MonoBehaviour
{
    // [SerializeField] private Animator playerAnim;
    [SerializeField] private AttributeSettings attributeSettings;
    [SerializeField] private SkillSettings skillSettings;

    // Start is called before the first frame update
    void Start()
    {
        // playerAnim = GetComponent<Animator>();
        // playerAnim.SetFloat("shift_no", skillSettings.ShiftNo); // burda animator yok ki.
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
        attributeSettings.SelectFastAttributes();
        // shootSpeed değişkenini de ayarla.

        // PlayerController.sprintSpeed = 8;
        // PlayerController.shContestSpeed = 3.2f;
        // ballController.shootSpeed = 17;
    }

    public void StrongStrikerSelected()
    {
        attributeSettings.SelectStrongAttributes();
        // shootSpeed değişkenini de ayarla.

        // PlayerController.sprintSpeed = 5.5f;
        // PlayerController.shContestSpeed = 4.2f;
        // ballController.shootSpeed = 27;
    }
}
