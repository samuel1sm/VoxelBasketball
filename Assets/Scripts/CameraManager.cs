using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CharacterStatus[] characters;
    [SerializeField] private Transform actualPlayer;
    private CinemachineVirtualCamera _camera;
    private BallManager _ballManager;
    private GameObject _auxPivot;

    private void Awake()
    {
        characters = FindObjectsOfType<CharacterStatus>();
        _camera = GetComponent<CinemachineVirtualCamera>();
        _ballManager = BallManager.Instance;
        _auxPivot = new GameObject {name = "pivot"};
        _ballManager.OnStateUpdated += UpdateCameraToBall;
    }

    private void UpdateCameraToBall(Transform obj)
    {
        // var ballTransform = _ballManager.transform;
        if (BallManager.ActualState == BallState.WasShoot)
        {
            print("bola");

            _camera.Follow = obj;
            _camera.LookAt = obj;
        }
        else if (BallManager.ActualState == BallState.ToBeCollect)
        {
            print("pivo");

            StartCoroutine(UpdatePivot(obj));
            _camera.Follow = _auxPivot.transform;
            _camera.LookAt = _auxPivot.transform;
        }
        else
        {
            var a = obj.GetComponent<CharacterStatus>();
            if (a.isAI)
            {
                print($"{a.gameObject.name}");
                StartCoroutine(UpdatePivot(obj));
                _camera.Follow = _auxPivot.transform;
                _camera.LookAt = _auxPivot.transform;
            }
            else
            {
                print("player");

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