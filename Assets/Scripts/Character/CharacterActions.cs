using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;
using Random = UnityEngine.Random;

// [RequireComponent(typeof(AnimationManager), typeof(InputManager))]
public class CharacterActions : MonoBehaviour
{
    // public event Action<Transform> HasTheBall = delegate(Transform o) { };

    [SerializeField] private float normalShootStaminaLost = 20f;
    [SerializeField] private Transform initialBallPosition;
    [SerializeField] private Transform hoopPosition;

    private CharacterStatus _characterStatus;
    private CharacterControls _characterControls;
    private AnimationManager _animationManager;
    private CharacterShootHandler _characterShootHandler;
    private float chanceResult;

    // private bool hasShoot;

    private void Awake()
    {
        _characterControls = GetComponent<CharacterControls>();
        _animationManager = GetComponent<AnimationManager>();
        _characterStatus = GetComponent<CharacterStatus>();
        _characterShootHandler = GetComponent<CharacterShootHandler>();
        // _hitMask = LayerMask.NameToLayer("Characters"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        _characterControls.FirstActionPressed += FirstActionHandle;
        _characterControls.SecondActionPressed += SecondActionHandle;

        _characterStatus.OnLostBall += LostTheBall;
        _characterStatus.OnCatchTheBall += HasTheBall;
        // _animationManager.OnAnimationEnd += HandleAnimationEnd;
    }

    private void HasTheBall(CharacterStatus obj)
    {
        _animationManager.UpdateStatusMode(true);
        _animationManager.StartFirstAction();
    }

    private void LostTheBall()
    {
        ChangeToDefence();
        _animationManager.SetWasHit();
    }

    public void Shoot()
    {
        bool scored = Random.Range(0f, 1f) <= chanceResult;
        BallManager.Instance.StartShoot(initialBallPosition.position, scored);
    }

    // private void HandleAnimationEnd(AnimationTypes obj)
    // {
    //     switch (obj)
    //     {
    //         case AnimationTypes.Jump:
    //             break;
    //         case AnimationTypes.Charge:
    //             break;
    //         case AnimationTypes.Shoot:
    //             bool scored = Random.Range(0f, 1f) <= chanceResult;
    //             BallManager.Instance.StartShoot(initialBallPosition.position, scored);
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
    //     }
    // }

    private void SecondActionHandle(ButtonInputTypes obj)
    {
        if (obj != ButtonInputTypes.Started) return;
        if (_characterStatus.GetHasTheBall())
        {
            //todo passe
        }
        else if (!_characterStatus.GetHasDash())
        {
            _animationManager.StartSecondAction();
            _characterStatus.UpdateHasCharged();
        }
    }


    private void FirstActionHandle(ButtonInputTypes obj)
    {
        switch (obj)
        {
            case ButtonInputTypes.Started:

                if (_characterStatus.GetHasTheBall())
                {
                    // if (hasShoot) return;

                    _animationManager.LookAt(hoopPosition.position, AxisConstraint.Y);
                    _characterStatus.UpdateMovementStatus(MovimentStatus.Stop);
                    _characterShootHandler.StartChanceBar();
                }
                else
                {
                    _animationManager.StartFirstAction();
                }

                break;


            case ButtonInputTypes.Canceled:
                if (_characterStatus.GetHasTheBall())
                {
                    // ChangeToDefence();
                    var hoopDistance = CalculateDistancePlayerToHoop();
                    var perc = hoopDistance * _characterStatus.GetStaminaPercentage();

                    chanceResult = _characterShootHandler.StopChanceBar(perc);

                    _animationManager.StartShootBallAnimation();
                    _characterStatus.UpdateHasTheBall(false);
                    ChangeToDefence();
                    _characterStatus.UpdateStamina(-normalShootStaminaLost * CalculateDistancePlayerToHoop());
                }

                break;
        }
    }


    private float CalculateDistancePlayerToHoop()
    {
        var result = Vector3.Distance(hoopPosition.position, initialBallPosition.position);
        result = Mathf.Clamp(result, 6, 24);
        return 1 - result / 24;
    }

    private void ChangeToDefence()
    {
        _animationManager.UpdateStatusMode(false);
        StartCoroutine(_characterStatus.EnableCatchTheBall());
    }
    
    
}