using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;


public class PlayerMovement : GenericMovement
{
    private CharacterController _characterController;

    private float _turnVelocity;

    private float _originalHeight;
    private InputManager _characterControls;
    [SerializeField] private float _speedPercentage = 1;

    protected override void Awake()
    {
        base.Awake();
        _characterController = GetComponent<CharacterController>();
        _characterControls = GetComponent<InputManager>();
        _originalHeight = transform.position.y;
    }

    private void Start()
    {
        // _characterControls.FirstActionPressed += ResetVelocity;
    }


    private void FixedUpdate()
    {
        var input = _characterControls.GetMovement();
        var direction = input.normalized;


        Vector3 velocity = direction * (characterSpeed * _speedPercentage);

        if (direction.magnitude >= 0.1f)
        {
            LoseStamina();


            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                ref _turnVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);


            Move(velocity);
            if (transform.position.y != _originalHeight)
            {
                var p = transform.position;
                p.y = _originalHeight;
                transform.position = p;
            }
        }
    }


    protected override void MovementActivation(MovimentStatus rule)
    {
        _speedPercentage = rule switch
        {
            MovimentStatus.Normal => 1,
            MovimentStatus.Slow => speedDecrease,
            MovimentStatus.Stop => 0,
        };
    }

    protected override void Move(Vector3 position)
    {
        _characterController.Move(position * Time.deltaTime);
    }
}