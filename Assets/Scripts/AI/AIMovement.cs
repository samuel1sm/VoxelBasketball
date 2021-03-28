using System;
using AI;
using UnityEngine;
using UnityEngine.AI;
using Utils;


public class AIMovement : GenericMovement
{
    [SerializeField] private float distanceToTarget = 2f;
    private NavMeshAgent _navMeshAgent;
    private AIBrain _aiBrain;
    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _aiBrain = GetComponent<AIBrain>();
        characterStatus = GetComponent<CharacterStatus>();
        distanceToTarget = _navMeshAgent.stoppingDistance;
        characterSpeed = _navMeshAgent.speed;
        _navMeshAgent.stoppingDistance = 0;
    }

    private void Start()
    {
        _aiBrain.OnModeUpdated += UpdateMovement;
    }

    private void UpdateMovement(AIMode obj)
    {
        switch (obj)
        {
            case AIMode.Defending:
                _navMeshAgent.stoppingDistance = distanceToTarget;
                break;
            case AIMode.Chasing:
                _navMeshAgent.stoppingDistance = 0;
                break;
            case AIMode.Lurking:
                break;
 
        }
    }


    private void FixedUpdate()
    {
        var position = _aiBrain.GetMovement();
        
        if (position.magnitude == 0) return;
        
        
        
        Move(position);
        LoseStamina();

    }

    protected override void Move(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
    }
}
