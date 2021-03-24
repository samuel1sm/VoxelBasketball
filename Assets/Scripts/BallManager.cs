using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public enum BallState
{
    WasShoot,WasCollected, ToBeCollect
}

public class BallManager : MonoBehaviour
{
    public static BallState ActualState = BallState.ToBeCollect;
    public event Action<BallState> StateUpdated = delegate(BallState shooted) {  };
        
    public static BallManager Instance;
    [SerializeField] private float ballSpeed = 0.5f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float ballRotation;

    private Vector3 _outMapPosition;
    private Rigidbody _rigidbody;
    private void Awake()
    {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _outMapPosition = Vector3.one * -10;
    }
    


    public void StartShoot(Vector3 initialPosition, Vector3 hoopPositionPosition)
    {
        
        
        StateChanged(BallState.WasShoot, true);
        
        transform.position = initialPosition;

        var direction = (hoopPositionPosition - initialPosition).normalized;
        
        transform.DOJump(hoopPositionPosition, jumpHeight, 1, ballSpeed)
            .onComplete += () => StateChanged(BallState.ToBeCollect, false);
        transform.DORotate(direction * ballRotation, ballSpeed, RotateMode.LocalAxisAdd);
        
    }

    private void StateChanged(BallState state, bool isKinematic)
    {
        ActualState = state;
        _rigidbody.isKinematic = isKinematic;
        StateUpdated(state);
    }

    

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && ActualState == BallState.ToBeCollect)
        {
            transform.position = _outMapPosition;
            StateChanged(BallState.WasCollected, true);
        }
    }

    
}
