using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{

    private InputManager _inputManager;
    private bool _isDefending = false;
    [SerializeField] private float ballHeight;
    [SerializeField] private float ballSpeed;
    [SerializeField] private Transform initialBallPosition;
    
    [SerializeField] private Transform hoopPosition;
    [SerializeField] private Transform ball;
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _inputManager.FirstActionPressed += FirstActionHandle;
    }

    private void FirstActionHandle(ButtonInputTypes obj)
    {
        switch (obj)
        {
            case ButtonInputTypes.Started:
            
                break;
            case ButtonInputTypes.Performed:
                if (!_isDefending)
                    Shoot();
                else
                    Defend();
                break;
            case ButtonInputTypes.Canceled:
                break;

        }
    }

    private void Defend()
    {
        
    }

    private void Shoot()
    {
        StartCoroutine(ShootBall());
    }
    

    IEnumerator ShootBall()
    {
        float animationTime = 0;

        var initial = initialBallPosition.position;
        while (true)
        {
            animationTime += Time.deltaTime;
            animationTime = animationTime % ballSpeed;
            
            ball.position = MathParabola.Parabola(initial, hoopPosition.position, ballHeight,
                animationTime / ballSpeed);
            
            yield return new WaitForSeconds(0f);
            
            if(animationTime >= ballSpeed) break;
        }
        
        print("saiu");
    }


}
