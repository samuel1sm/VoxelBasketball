using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(InputManager))]
public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float playerSpeed;
    [SerializeField] private float turnSmoothTime;
    private InputManager _inputManager;
    private CharacterController _characterController;
    private float _turnVelocity;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _inputManager = GetComponent<InputManager>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
   

    private void FixedUpdate()
    {
        var input = _inputManager.GetPlayerMovement();
        var direction = Vector22Vector3(input).normalized ;


        if (direction.magnitude >= 0.1f)
        {

            var targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, 
                ref _turnVelocity , turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            _characterController.Move(direction * (Time.deltaTime * playerSpeed));    
            
        }
        
    }

    private Vector3 Vector22Vector3(Vector2 vector2)
    {
        return new Vector3(vector2.x, 0, vector2.y);
    }
}
