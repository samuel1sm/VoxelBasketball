using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator _animator;
    private static readonly int Shoot = Animator.StringToHash("Shoot");
    private static readonly int HasTheBall = Animator.StringToHash("HasTheBall");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    public void StartShootBallAnimation()
    {
        _animator.SetTrigger(Shoot);
    }
    
    public void StartBouncing()
    {
        _animator.SetTrigger(HasTheBall);
    }
}
