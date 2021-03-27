using System;
using UnityEngine;
using UnityEngine.Serialization;
using Utils;

namespace AI
{
    public class AIBrain : CharacterControls
    {
        [SerializeField] private Transform positionToGo;
        [SerializeField] private Transform hoop;
        private bool IsChansingTheBall { get; set; } = true;
        private Transform _previousPosition;
        private CharacterActions _characterActions;
        private BallManager _ballManager;
        
        public event Action<bool> OnChansigBallUpdated = delegate(bool b) {  };
        private void Awake()
        {
            _ballManager = BallManager.Instance;
            _characterActions = GetComponent<CharacterActions>();
        }

        private void Start()
        {
            _characterActions.HasTheBall += IsAttacking;
            _ballManager.StateUpdated += HandleBallUpdate;
            positionToGo = _ballManager.transform;

        }

        private void HandleBallUpdate(BallState obj)
        {
            if (obj == BallState.ToBeCollect)
            {
                IsChansingTheBall = true;
                positionToGo = _ballManager.transform;
            }
            else
            {
                IsChansingTheBall = false;
            }

            OnChansigBallUpdated(IsChansingTheBall);

        }

        private void IsAttacking(Transform obj)
        {
            positionToGo = hoop;
        }

   
        
        public override void MovementActivation(ButtonInputTypes types)
        {
            if (types == ButtonInputTypes.Started)
            {
                _previousPosition = positionToGo;
                positionToGo = transform;
            }
            else
                positionToGo = _previousPosition;
            
            
        }

        public void SetMovement(Transform position)
        {
            positionToGo = position;
        }
        
        public override Vector3 GetMovement()
        {
            return positionToGo.position;
        }
    }
}