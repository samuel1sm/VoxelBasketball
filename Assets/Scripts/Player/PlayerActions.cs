using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [RequireComponent(typeof(AnimationManager), typeof(InputManager))]
public class PlayerActions : MonoBehaviour
{
    
    public event Action<Transform> HasTheBall = delegate(Transform o) {  };
    private InputManager _inputManager;
    private bool _isAttacking = false;
    

    [SerializeField] private Transform initialBallPosition;
    [SerializeField] private Transform hoopPosition;

    private AnimationManager _animationManager;
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _animationManager =GetComponent<AnimationManager>();

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
                if (_isAttacking)
                {
                    _animationManager.StartShootBallAnimation();
                    // _isAttacking = false;
                    StartCoroutine(ChangeToDefence());
                }
                else
                {
                    // _isAttacking = true;

                    Defend();
                }
                break;

            case ButtonInputTypes.Performed:
                break;

            case ButtonInputTypes.Canceled:
                break;

        }
    }

    private void Defend()
    {
        
    }

    public void Shoot()
    {
        
        BallManager.Instance.StartShoot(initialBallPosition.position,hoopPosition.position);
        // StartCoroutine(ShootBall());
    }

    IEnumerator ChangeToDefence()
    {
        yield return new WaitForSeconds(0.4f);
        _isAttacking = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !_isAttacking)
        {
            HasTheBall(transform);
            _isAttacking = true;
            _animationManager.StartBouncing();
        }
    }
}
