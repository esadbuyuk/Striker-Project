using UnityEngine;

[CreateAssetMenu(menuName = "Attribute/Settings", fileName = "PlayerData")]
public class AttributeSettings : ScriptableObject
{
    [SerializeField] private float turnSpeed = 50f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float sholContestSpeed = 3.2f;

    // [SerializeField] private bool useAi = false;

    public float TurnSpeed { get { return turnSpeed; } }
    public float SprintSpeed { get { return sprintSpeed; } }
    public float SholContestSpeed { get { return sholContestSpeed; } }

    // public bool UseAi { get { return useAi; } }
}
