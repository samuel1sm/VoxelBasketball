using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CharacterActions[] players;
    private BallManager _ballManager;
    private CinemachineVirtualCamera _camera;
    private Transform _lastPlayer;
    private GameObject _auxPivot;
    private void Awake()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _ballManager = BallManager.Instance;
        foreach (var player in players)
        {
            player.HasTheBall += UpdateCameraToCharacter;
        }

        _auxPivot = new GameObject {name = "pivot"};
        _ballManager.StateUpdated += UpdateCameraToBall;
        
    }

    private void UpdateCameraToBall(BallState obj)
    {
        
        
        switch (obj)
        {
            case BallState.WasShoot:
                var ballTransform = _ballManager.transform;
                _camera.Follow = ballTransform;
                _camera.LookAt = ballTransform;
                break;
            case BallState.ToBeCollect:
            {
                StartCoroutine(UpdatePivot());
                _camera.Follow = _auxPivot.transform;
                _camera.LookAt = _auxPivot.transform;
                break;
            }
        }
    }

    IEnumerator UpdatePivot()
    {
        var ballTransform = _ballManager.transform;

        while (true)
        {
            yield return new WaitForSeconds(0f);
            var position = _lastPlayer.position;
            _auxPivot.transform.position = (ballTransform.position - position) / 2 + position;
        }
    }

    private void UpdateCameraToCharacter(Transform obj)
    {
        StopCoroutine( UpdatePivot());
        _lastPlayer = obj;
        _camera.Follow = obj;
        _camera.LookAt = obj;
    }
}
