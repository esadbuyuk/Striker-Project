using UnityEngine;

[CreateAssetMenu(menuName = "SkillSettings", fileName = "SkillsData")]
public class SkillSettings : ScriptableObject
{
    [SerializeField] private int dodgeNo = 0;

    public float DodgeNo { get { return dodgeNo; } }

    public void NextDodge()
    {
        dodgeNo += 1;
    }    

    // public bool UseAi { get { return useAi; } }
}
