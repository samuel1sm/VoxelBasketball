using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputManager))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float stamina2Lose = 1f;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float turnSmoothTime;
    [SerializeField] private float gravity;
    private InputManager _inputManager;
    private CharacterController _characterController;
    private CharacterStatus _characterStatus;

    private float _turnVelocity;

    private float _velocityY;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputManager = GetComponent<InputManager>();
        _characterStatus = GetComponent<CharacterStatus>();
    }

    private void Start()
    {
        _inputManager.FirstActionPressed += ResetVelocity;
    }

    private void ResetVelocity(ButtonInputTypes obj)
    {
        if (obj == ButtonInputTypes.Canceled)
            _velocityY = 0;
    }


    private void FixedUpdate()
    {
        var input = _inputManager.GetPlayerMovement();
        var direction = Vector22Vector3(input).normalized;

        // if (_characterController.isGrounded)
        //     _velocityY = 0.0f;
        // _velocityY += gravity;

        Vector3 velocity = direction * playerSpeed + Vector3.down * _velocityY;


        if (direction.magnitude >= 0.1f)
        {
            _characterStatus.UpdateStamina(-stamina2Lose);
            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle,
                ref _turnVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            // var movement = direction * (Time.deltaTime * playerSpeed);
            //
            // _characterController.Move(movement);   
        }
        print(velocity * Time.deltaTime);
        _characterController.Move(velocity * Time.deltaTime);

        // _rigidbody.velocity = Vector3.down * gravity; 
    }

    private Vector3 Vector22Vector3(Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }
}