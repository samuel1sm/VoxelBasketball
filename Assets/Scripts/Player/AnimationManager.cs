using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int FirstAction = Animator.StringToHash("FirstAction");
    private bool _isAttacking;
    private static readonly int IsAttacking = Animator.StringToHash("isAttacking");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    
    public void StartShootBallAnimation()
    {
        _animator.SetTrigger(Shoot);
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
}
