using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;


public enum BallState
{
    WasShoot,
    WasCollected,
    ToBeCollect
}

public class BallManager : MonoBehaviour
{
    public static BallState ActualState = BallState.ToBeCollect;
    public event Action<BallState> StateUpdated = delegate(BallState shooted) { };

    public static BallManager Instance;
    [SerializeField] private float ballSpeed = 0.5f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float ballRotation;
    [SerializeField] private List<Waypoint> waypointInfosList;

    [SerializeField] private Transform hoopPosition;
    [SerializeField] private Transform errorPosition;

    private Vector3 _outMapPosition;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        Instance = this;
        _rigidbody = GetComponent<Rigidbody>();
        _outMapPosition = Vector3.one * -10;
    }


    public void StartShoot(Vector3 initialPosition, bool scored)
    {
        StateChanged(BallState.WasShoot, true);

        transform.position = initialPosition;

        var position = hoopPosition.position;
        var direction = (position - initialPosition).normalized;

        // var path = waypointInfosList[0];
        // Vector3[] positions = new Vector3[path.pathPoints.Length + 1];
        // positions[0] = transform.position;
        // for (int i = 0; i < path.pathPoints.Length; i++)
        // {
        //     positions[i + 1] = path.pathPoints[i].position;
        // }
        //       
        // print(positions.Length);
        // transform.DOPath(positions, ballSpeed, path.type);

        if (scored)
        {
            transform.DOJump(position, jumpHeight, 1, ballSpeed)
                .onComplete += () => StateChanged(BallState.ToBeCollect, false);
        }
        else
        {
            transform.DOMove(errorPosition.position, ballSpeed )
                .onComplete += () => StateChanged(BallState.ToBeCollect, false);
        }

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