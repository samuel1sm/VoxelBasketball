using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// [RequireComponent(typeof(AnimationManager), typeof(InputManager))]
public class PlayerActions : MonoBehaviour
{
    public event Action<Transform> HasTheBall = delegate(Transform o) { };
    private InputManager _inputManager;
    private bool _isAttacking = false;

    [SerializeField] private float normalShootStaminaLost = 20f;
    [SerializeField] private Transform initialBallPosition;
    [SerializeField] private Transform hoopPosition;
    private CharacterStatus _characterStatus;

    private AnimationManager _animationManager;
    private float chanceResult;
    
    private void Awake()
    {
        _inputManager = GetComponent<InputManager>();
        _animationManager = GetComponent<AnimationManager>();
        _characterStatus = GetComponent<CharacterStatus>();
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
                    _inputManager.MovementActivation(obj);
                    _characterStatus.StartChanceBar();
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
                if (_isAttacking)
                {
                    var hoopDistance = CalculateDistancePlayerToHoop();

                    chanceResult = _characterStatus.StopChanceBar(hoopDistance);
                    _animationManager.StartShootBallAnimation();
                    
                    _characterStatus.UpdateStamina(-normalShootStaminaLost * CalculateDistancePlayerToHoop());
                    StartCoroutine(ChangeToDefence());
                    _inputManager.MovementActivation(obj);
                }

                break;
        }
    }

    private void Defend()
    {
    }

    public void Shoot()
    {
        bool scored = Random.Range(0f, 1f) <= chanceResult; 
        BallManager.Instance.StartShoot(initialBallPosition.position, scored);
        // StartCoroutine(ShootBall());
    }

    private float CalculateDistancePlayerToHoop()
    {
        var result = Vector3.Distance(hoopPosition.position, initialBallPosition.position);
        result = Mathf.Clamp(result, 6, 24);
        return 1 - result / 24;
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