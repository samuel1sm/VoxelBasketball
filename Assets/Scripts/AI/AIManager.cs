using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;
using UnityEngine.Serialization;


public class AIManager : MonoBehaviour
{
    [SerializeField] private AIBrain[] aiBrains;
    private Transform _playerWithBall;
    private BallManager _manager;

    private void Awake()
    {
        aiBrains = GetComponentsInChildren<AIBrain>();
        _manager = BallManager.Instance;
    }

    private void Start()
    {
        _manager.OnStateUpdated += HandleBallUpdate;
    }

    private void HandleBallUpdate(Transform obj)
    {
        if (BallManager.ActualState == BallState.ToBeCollect)
        {
            if (aiBrains.Length == 1)
            {
                aiBrains[0].SetMovement(_manager.transform);
                aiBrains[0].UpdateAIMode(AIMode.Chasing);
            }

            else
            {
                
            }
        }
        else if (BallManager.ActualState == BallState.WasCollected)
        {
            var status = obj.GetComponentInParent<CharacterStatus>();
            if (status.isAI) return;
        
            _playerWithBall = obj.transform;
            if (aiBrains.Length == 1)
            {
                aiBrains[0].SetMovement(_playerWithBall.transform);
                aiBrains[0].UpdateAIMode(AIMode.Defending);
            }
        }
    }
}