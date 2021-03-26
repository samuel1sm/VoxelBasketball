using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum ButtonInputTypes{
    Started, Performed, Canceled
}

public class InputManager : MonoBehaviour
{
    private PlayerInputs _playerInput;
    
    public event Action<ButtonInputTypes> FirstActionPressed = delegate(ButtonInputTypes types) {  }; 

    private void Awake()
    {
        _playerInput = new PlayerInputs();
    }

    private void Start()
    {
        _playerInput.Actions.FirstAction.started += _ => FirstActionPressed(ButtonInputTypes.Started);
        _playerInput.Actions.FirstAction.performed += _ => FirstActionPressed(ButtonInputTypes.Performed);
        _playerInput.Actions.FirstAction.canceled += _ => FirstActionPressed(ButtonInputTypes.Canceled);
        
        // _playerInput.Actions.FirstAction.started += _ => MovementActivation(ButtonInputTypes.Started);
        // _playerInput.Actions.FirstAction.canceled += _ => MovementActivation(ButtonInputTypes.Canceled);

    }

    public void MovementActivation(ButtonInputTypes types)
    {
        if (types == ButtonInputTypes.Started)
        {
            _playerInput.Actions.Movement.Disable();
        }
        else
        {
            _playerInput.Actions.Movement.Enable();
        }
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return _playerInput.Actions.Movement.ReadValue<Vector2>();
    }


}
