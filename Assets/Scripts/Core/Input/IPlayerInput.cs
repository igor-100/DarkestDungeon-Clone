using System;
using UnityEngine;

public interface IPlayerInput
{
    event Action Fire;
    event Action Escape;
    event Action<Vector3> MousePositionUpdated;
    event Action<Vector2> Move;
    event Action ShiftPressed;
    event Action ShiftReleased;
    event Action Jump;

    void Disable();
    void Enable();
}
