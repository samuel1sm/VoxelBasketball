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

        
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    // Start is called before the first frame update
    public Vector2 GetPlayerMovement()
    {
        return _playerInput.Actions.Movement.ReadValue<Vector2>();
    }

// Update is called once per frame
    void Update()
    {
        
    }
}
