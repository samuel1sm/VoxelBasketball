using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;
using UnityEngine.Serialization;


public class AIManager : MonoBehaviour
{
    [SerializeField] private CharacterActions[] playersActions;
    [SerializeField] private AIBrain[] aiBrains;
    private BallManager _ballManager;
    private Transform _playerWithBall;
    
    private void Awake()
    {
        aiBrains = GetComponentsInChildren<AIBrain>();
        _ballManager = BallManager.Instance;
      
        
    }

    private void Start()
    {
        foreach (var player in playersActions)
        {
            player.HasTheBall += HandlePlayerWithBall;
        }
    }

    private void HandlePlayerWithBall(Transform obj)
    {
        _playerWithBall = obj;
        print(obj.gameObject.name);
        if(aiBrains.Length == 1)
            aiBrains[0].SetMovement(_playerWithBall.transform);
    }

  
}
