using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;


public class PlayerMovement : GenericMovement
{
    private CharacterController _characterController;

    private float _turnVelocity;
    private float _velocityY;
    private InputManager _characterControls;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _characterControls = GetComponent<InputManager>();
        characterStatus = GetComponent<CharacterStatus>();
    }

    private void Start()
    {
        _characterControls.FirstActionPressed += ResetVelocity;
    }

    private void ResetVelocity(ButtonInputTypes obj)
    {
        if (obj == ButtonInputTypes.Canceled)
            _velocityY = 0;
    }


    private void FixedUpdate()
    {
        var input = _characterControls.GetMovement();
        var direction = input.normalized;


        Vector3 velocity = direction * characterSpeed + Vector3.down * _velocityY;


        if (direction.magnitude >= 0.1f)
        {
            LoseStamina();
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                ref _turnVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            
            Move(velocity);
        }
    }


    protected override void Move(Vector3 position)
    {
        _characterController.Move(position * Time.deltaTime);
    }
}