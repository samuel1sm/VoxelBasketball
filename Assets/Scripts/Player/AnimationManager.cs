using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public enum AnimationTypes{
    Jump, Charge, Shoot
}
public class AnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    public event Action<AnimationTypes> OnAnimationEnd = delegate {  };
    private bool _isAttacking;
    private Animator _animator;
    private static readonly int ShootTag = Animator.StringToHash("Shoot");
    private static readonly int FirstAction = Animator.StringToHash("FirstAction");
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");
    private static readonly int SecondAction = Animator.StringToHash("SecondAction");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    
    public void StartShootBallAnimation()
    {
        _animator.SetTrigger(ShootTag);
    }
    
    public void LookAt(Vector3 position, AxisConstraint axisLocker)
    {
        transform.DOLookAt(position, 0.6f, axisLocker);
    }
        

    public void UpdateStatus(bool status)
    {
        _animator.SetBool(IsAttacking, status);
    }
    
    public void StartFirstAction()
    {
        _animator.SetTrigger(FirstAction);
    }
    
    public void StartSecondAction()
    {
        _animator.SetTrigger(SecondAction);
    }

    public void Jump()
    {
        var originalPosition = transform.position;

        DOTween.Sequence()
            .Append(transform.DOMove((originalPosition + transform.up), 0.3f).SetEase(Ease.Linear))
            .Append(transform.DOMove(originalPosition, 0.3f)).SetEase(Ease.Linear)
            .onComplete += () => OnAnimationEnd(AnimationTypes.Jump);
    }

    public void Charge()
    {
        var originalPosition = transform.position;
        DOTween.Sequence()
            .Append(transform.DOMove(transform.position + transform.forward * 2, 0.9f).SetEase(Ease.Linear))
            .Append(transform.DOMove(originalPosition, 0.11f).SetEase(Ease.Linear))
        
            .onComplete += () => OnAnimationEnd(AnimationTypes.Charge);
    }

    public void Shoot()
    {
        OnAnimationEnd(AnimationTypes.Shoot);
    }
    
}
