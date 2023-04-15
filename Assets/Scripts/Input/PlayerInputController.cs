using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class PlayerInputController : MonoBehaviour
{
    private PlayerInputActions _playerInputActions;

    public event EventHandler OnShootAction;


    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        
        _playerInputActions.Play.Shoot.performed += OnShootPerformed;
    }

    private void OnShootPerformed(InputAction.CallbackContext context) 
    {
        OnShootAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMousePosition()
    {
        return _playerInputActions.Play.Mouse.ReadValue<Vector2>();
    }

    public Vector2 GetMove()
    {
        return _playerInputActions.Play.Move.ReadValue<Vector2>();
    }

    public Vector2 GetRJoystick()
    {
        return _playerInputActions.Play.RJoystickLook.ReadValue<Vector2>();
    }
}
