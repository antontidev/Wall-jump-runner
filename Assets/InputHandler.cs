using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class InputHandler : MonoBehaviour
{
    [Inject]
    private IPlayerMover playerMover;

    public void JoystickInput_Changed(Vector2 value)
    {
        playerMover.Move = value;
    }

    public void JoystickButtonA_Clicked()
    {
        playerMover.Attack = true;
    }

    public void JoystickButtonA_Up()
    {
        playerMover.Attack = false;
    }

    public void JoystickButtonB_Clicked()
    {
        playerMover.Jump = true;
    }

    public void JoystickButtonB_Up()
    {
        playerMover.Jump = false;
    }
}
