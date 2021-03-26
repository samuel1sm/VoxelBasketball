using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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
                    _animationManager.LookAt(hoopPosition.position, AxisConstraint.Y);
                    _inputManager.MovementActivation(obj);
                    _characterStatus.StartChanceBar();
                }
                else
                {
                    _inputManager.MovementActivation(obj);
                    _animationManager.StartFirstAction();
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


    public void Shoot()
    {
        bool scored = Random.Range(0f, 1f) <= chanceResult;
        BallManager.Instance.StartShoot(initialBallPosition.position, scored);
        // StartCoroutine(ShootBall());
    }

    public void Jump()
    {
        var originalPosition = transform.position;
        
        DOTween.Sequence()
            .Append(transform.DOMove((originalPosition + transform.up), 0.3f).SetEase(Ease.Linear))
            .Append(transform.DOMove(originalPosition, 0.3f)).SetEase(Ease.Linear)
            .onComplete += () => _inputManager.MovementActivation(ButtonInputTypes.Canceled);

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
        _animationManager.UpdateStatus(_isAttacking);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && !_isAttacking)
        {
            HasTheBall(transform);
            _isAttacking = true;
            _animationManager.UpdateStatus(_isAttacking);

            _animationManager.StartFirstAction();
        }
    }
}