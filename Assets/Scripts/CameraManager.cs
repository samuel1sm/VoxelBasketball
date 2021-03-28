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
        foreach (var player in characters)
        {
            player.OnCatchTheBall += UpdateCameraToCharacter;
        }

        _auxPivot = new GameObject {name = "pivot"};
        _ballManager.StateUpdated += UpdateCameraToBall;
    }

    private void UpdateCameraToBall(BallState obj)
    {
        var ballTransform = _ballManager.transform;
        switch (obj)
        {
            case BallState.WasShoot:
                _camera.Follow = ballTransform;
                _camera.LookAt = ballTransform;
                break;
            case BallState.ToBeCollect:
            {
                StartCoroutine(UpdatePivot(ballTransform));
                _camera.Follow = _auxPivot.transform;
                _camera.LookAt = _auxPivot.transform;
                break;
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

    private void UpdateCameraToCharacter(CharacterStatus obj)
    {
        if (obj.isAI)
        {
            StartCoroutine(UpdatePivot(obj.transform));
            _camera.Follow = _auxPivot.transform;
            _camera.LookAt = _auxPivot.transform;
            return;
        }

        StopCoroutine("UpdatePivot");
        var objTransform = obj.transform;
        actualPlayer = objTransform;
        _camera.Follow = objTransform;
        _camera.LookAt = objTransform;
    }
}