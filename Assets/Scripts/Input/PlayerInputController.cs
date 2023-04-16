using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Video;

public class PlayerInputController : MonoBehaviour
{
    public static PlayerInputController Instance;

    private PlayerInputActions _playerInputActions;

    public event EventHandler OnShootActionPerformed;
    public event EventHandler OnShootActionReleased;
    
    private void Awake()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        
        _playerInputActions.Play.Shoot.performed += OnShootPerformed;
        _playerInputActions.Play.Shoot.canceled += OnShootReleased;

        Instance = this;
    }

    private void OnShootPerformed(InputAction.CallbackContext context) 
    {
        OnShootActionPerformed?.Invoke(this, EventArgs.Empty);
    }
    
    private void OnShootReleased(InputAction.CallbackContext context) 
    {
        OnShootActionReleased?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMousePosition()
    {
        return _playerInputActions.Play.Mouse.ReadValue<Vector2>();
    }

    public Vector2 GetMove()
    {
        Vector2 input = _playerInputActions.Play.Move.ReadValue<Vector2>();
        
            
        if (Mathf.Abs(input.x) < 0.15f)
            input.x = 0;
            
        if (Mathf.Abs(input.y) < 0.15f)
            input.y = 0;

        return input;
    }

    public Vector2 GetRJoystick()
    {
        Vector2 input = _playerInputActions.Play.RJoystickLook.ReadValue<Vector2>();
            
        if (Mathf.Abs(input.x) < 0.15f)
            input.x = 0;
            
        if (Mathf.Abs(input.y) < 0.15f)
            input.y = 0;
        
        return input;
    }

    public bool GetLook()
    {
        return _playerInputActions.Play.Look.ReadValue<float>() > 0;
    }
}
