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

        _navMeshAgent.stoppingDistance = 0;
    }

    private void Start()
    {
        _aiBrain.OnChansigBallUpdated += b => _navMeshAgent.stoppingDistance = b ? 0 : distanceToTarget;
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
