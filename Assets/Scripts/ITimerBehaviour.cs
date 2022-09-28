using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimerBehaviour
{
    public bool IsStealing { get; }

    public void SetMovementController(MovementController mc);

    public void ActivateTimerBehaviour();

    public void Stun();

    public void Shake();

}
