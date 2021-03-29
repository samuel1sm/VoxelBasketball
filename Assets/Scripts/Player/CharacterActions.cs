using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
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
    private float chanceResult;
    private bool hasAttacked = false;

    private void Awake()
    {
        _characterControls = GetComponent<CharacterControls>();
        _animationManager = GetComponent<AnimationManager>();
        _characterStatus = GetComponent<CharacterStatus>();
        // _hitMask = LayerMask.NameToLayer("Characters"); 
    }

    // Start is called before the first frame update
    void Start()
    {
        _characterControls.FirstActionPressed += FirstActionHandle;
        _characterControls.SecondActionPressed += SecondActionHandle;
        _characterStatus.OnBallStollen += LostTheBall;
        _characterStatus.OnCatchTheBall += HasTheBall;

        _animationManager.OnAnimationEnd += HandleAnimationEnd;
    }

    private void HasTheBall(CharacterStatus obj)
    {
        _animationManager.UpdateStatus(true);
        _animationManager.StartFirstAction();
    }

    private void LostTheBall()
    {
        _animationManager.UpdateStatus(false);
        _animationManager.LoseTheBall();
    }


    private void HandleAnimationEnd(AnimationTypes obj)
    {
        switch (obj)
        {
            case AnimationTypes.Jump:
                _characterControls.MovementActivation(ButtonInputTypes.Canceled);
                break;
            case AnimationTypes.Charge:
                hasAttacked = !hasAttacked;

                _characterControls.MovementActivation(ButtonInputTypes.Canceled);
                break;
            case AnimationTypes.Shoot:
                bool scored = Random.Range(0f, 1f) <= chanceResult;
                BallManager.Instance.StartShoot(initialBallPosition.position, scored);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(obj), obj, null);
        }
    }

    private void SecondActionHandle(ButtonInputTypes obj)
    {
        if (obj != ButtonInputTypes.Started) return;

        if (_characterStatus.GetHasTheBall())
        {
            //todo passe
        }
        else
        {

            hasAttacked = !hasAttacked;
            _characterControls.MovementActivation(obj);

            _animationManager.StartSecondAction();
        }
    }


    private void FirstActionHandle(ButtonInputTypes obj)
    {
        switch (obj)
        {
            case ButtonInputTypes.Started:
                _characterControls.MovementActivation(obj);

                if (_characterStatus.GetHasTheBall())
                {
                    _animationManager.LookAt(hoopPosition.position, AxisConstraint.Y);
                    _characterStatus.StartChanceBar();
                }
                else
                {
                    _animationManager.StartFirstAction();
                }

                break;
            

            case ButtonInputTypes.Canceled:
                if (_characterStatus.GetHasTheBall())
                {
                    var hoopDistance = CalculateDistancePlayerToHoop();

                    chanceResult = _characterStatus.StopChanceBar(hoopDistance);
                    _animationManager.StartShootBallAnimation();

                    _characterStatus.UpdateStamina(-normalShootStaminaLost * CalculateDistancePlayerToHoop());
                    StartCoroutine(ChangeToDefence());
                    _characterControls.MovementActivation(obj);
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

    IEnumerator ChangeToDefence()
    {
        yield return new WaitForSeconds(0.4f);

        _characterStatus.UpdateIsAttacking();
        _animationManager.UpdateStatus(false);
    }
}