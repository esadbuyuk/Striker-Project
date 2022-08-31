using UnityEngine;

[CreateAssetMenu(menuName = "SkillSettings", fileName = "SkillsData")]
public class SkillSettings : ScriptableObject
{    
    [SerializeField] private int shiftNo = 0;
    public float ShiftNo { get { return shiftNo; } }


    public void NextDodge()
    {
        shiftNo += 1;
        HandleShiftAnimations();
    }

    private void HandleShiftAnimations()
    {
        switch (shiftNo)
        {
            case 0:
                shiftNo = 0;
                // shift = "chop";
                break;
            case 1:
                shiftNo = 1;
                // shift = "ronaldo_chop";
                break;
            case 2:
                shiftNo = 2;
                // shift = "messi_chop";
                break;
        }
    }
}
