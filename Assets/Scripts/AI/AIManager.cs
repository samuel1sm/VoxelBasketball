using System;
using System.Collections;
using System.Collections.Generic;
using AI;
using UnityEngine;
using UnityEngine.Serialization;


public class AIManager : MonoBehaviour
{
    [SerializeField] private AIBrain[] aiBrains;
    [SerializeField] private Transform hoopPostion;
    private Transform _playerWithBall;
    private BallManager _ballManager;

    private void Awake()
    {
        aiBrains = GetComponentsInChildren<AIBrain>();
        _ballManager = BallManager.Instance;
        _ballManager.OnStateUpdated += HandleBallUpdate;

    }

    private void Start()
    {
    }

    private void HandleBallUpdate(Transform obj)
    {
        if (BallManager.ActualState == BallState.ToBeCollect)
        {
            if (aiBrains.Length == 1)
            {
                aiBrains[0].SetMovement(_ballManager.transform);
                aiBrains[0].UpdateAIMode(AIMode.Chasing);
            }

            else
            {
                
            }
        }
        else if (BallManager.ActualState == BallState.WasCollected)
        {
            var status = obj.GetComponentInParent<CharacterStatus>();
            if (status.isAI)
            {
                foreach (var ai in aiBrains)
                {
                    if (status.CharacterID == ai.GetComponent<CharacterStatus>().CharacterID)
                    {
                        ai.SetMovement(hoopPostion);
                        ai.UpdateAIMode(AIMode.Advancing);
                    }
                }
                return;
            }
                
        
            _playerWithBall = obj.transform;
            if (aiBrains.Length == 1)
            {
                aiBrains[0].SetMovement(_playerWithBall.transform);
                aiBrains[0].UpdateAIMode(AIMode.Defending);
            }
        }
    }
}