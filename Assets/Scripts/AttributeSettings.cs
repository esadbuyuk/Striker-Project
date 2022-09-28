using UnityEngine;

[CreateAssetMenu(menuName = "Attribute/Settings", fileName = "PlayerData")]
public class AttributeSettings : ScriptableObject
{
    private string defenderName;
    [SerializeField] private string defenderType;
    [SerializeField] private int level = 0;
    [SerializeField] private float turnSpeed = 50f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float sholContestSpeed = 3.2f;

    public float TurnSpeed { get { return turnSpeed; } }
    public float SprintSpeed { get { return sprintSpeed; } }
    public float SholContestSpeed { get { return sholContestSpeed; } }
    public string DefenderName { get { return defenderName; } }


    public void SelectStrongAttributes()
    {
        sprintSpeed = 6f;
        sholContestSpeed = 4f;
    }

    public void SelectFastAttributes()
    {
        sprintSpeed = 11f;
        sholContestSpeed = 3f;
    }

    public void SelectNormalAttributes()
    {
        sprintSpeed = 8f;
        sholContestSpeed = 4f;
    }

    public void LevelUp()
    {
        level += 1;
        sprintSpeed += 1f;
        defenderName = "lvl " + level.ToString() + " " + defenderType + " Defender";
    }

    public void ResetLevel()
    {
        level = 1;
    }

    // public bool UseAi { get { return useAi; } }
}
