using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerInput _heroInput;

    private void Awake()
    {
        _heroInput = new PlayerInput();
        _heroInput.Enable();
    }

    public Vector2 GetMoveDirection()
    {
        return _heroInput.Player.Move.ReadValue<Vector2>();
    }
}
