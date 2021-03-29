using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform actualPlayer;
    private CinemachineVirtualCamera _camera;
    private BallManager _ballManager;
    private GameObject _auxPivot;
    private Coroutine _pivotCoroutine;

    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _ballManager = BallManager.Instance;
        _auxPivot = new GameObject {name = "pivot"};
        _ballManager.OnStateUpdated += UpdateCameraToBall;
    }

    private void UpdateCameraToBall(Transform obj)
    {
        if (_pivotCoroutine != null)
            StopCoroutine(_pivotCoroutine);

        // var ballTransform = _ballManager.transform;
        if (BallManager.ActualState == BallState.WasShoot)
        {
            _camera.Follow = obj;
            _camera.LookAt = obj;
        }
        else if (BallManager.ActualState == BallState.ToBeCollect)
        {
            _pivotCoroutine = StartCoroutine(UpdatePivot(obj));
            _camera.Follow = _auxPivot.transform;
            _camera.LookAt = _auxPivot.transform;
        }
        else
        {
            var a = obj.GetComponent<CharacterStatus>();

            if (a.isAI)
            {
                _pivotCoroutine = StartCoroutine(UpdatePivot(obj));
                _camera.Follow = _auxPivot.transform;
                _camera.LookAt = _auxPivot.transform;
            }
            else
            {
                _camera.Follow = obj;
                _camera.LookAt = obj;
            }
        }
    }

    IEnumerator UpdatePivot(Transform objectToLook)
    {
        while (true)
        {
            yield return new WaitForSeconds(0f);
            var position = actualPlayer.position;
            _auxPivot.transform.position = (objectToLook.position - position) / 2 + position;
        }
    }
}