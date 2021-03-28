using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Utils;

namespace AI
{
    public enum AIMode
    {
        Advancing,
        Defending,
        Chasing,
        Lurking,
        Escaping
    }

    public class AIBrain : CharacterControls
    {
        [SerializeField] private Transform positionToGo;
        [SerializeField] private Transform hoop;
        private Transform _previousPosition;
        private CharacterStatus _characterStatus;
        private BallManager _ballManager;
        private AIMode _aiMode = AIMode.Advancing;

        public event Action<AIMode> OnModeUpdated = delegate(AIMode b) { };

        private void Awake()
        {
            _ballManager = BallManager.Instance;
            _characterStatus = GetComponent<CharacterStatus>();
        }

        private void Start()
        {
            // _characterStatus.OnBallStollen += () => UpdateAIMode(AIMode.Defending);
            _characterStatus.OnCatchTheBall += _ => UpdateAIMode(AIMode.Advancing);
            // _ballManager.OnStateUpdated += HandleBallUpdate;
            positionToGo = _ballManager.transform;
        }

        public void UpdateAIMode(AIMode aiMode)
        {
            _aiMode = aiMode;
            switch (aiMode)
            {
                case AIMode.Advancing:
                    positionToGo = hoop;
                    break;
                case AIMode.Defending:
                    break;
                case AIMode.Chasing:
                    positionToGo = _ballManager.transform;
                    break;
                case AIMode.Lurking:
                    break;
                case AIMode.Escaping:
                    break;
            }

            OnModeUpdated(_aiMode);
        }


   

        private void Update()
        {
            switch (_aiMode)
            {
                case AIMode.Advancing:
                    break;
                case AIMode.Defending:
                    HandleDefence();
                    break;
                case AIMode.Chasing:
                    break;
                case AIMode.Lurking:
                    break;
                case AIMode.Escaping:
                    break;
            }
        }

        private void HandleDefence()
        {
            // print($"{positionToGo.position} {positionToGo.gameObject.name}");
            // if (Vector3.Distance(positionToGo.position, transform.position) <=
            //     GetComponent<NavMeshAgent>().stoppingDistance)
            // {
            //     // transform.LookAt(positionToGo.position);
            //     transform.DOLookAt(positionToGo.position, 0.2F, AxisConstraint.Y);
            //     OnSecondActionPressed(ButtonInputTypes.Started);
            // }
        }


        public override void MovementActivation(ButtonInputTypes types)
        {
            // if (types == ButtonInputTypes.Started)
            // {
            //     _previousPosition = positionToGo;
            //     positionToGo = transform;
            // }
            // else
            //     positionToGo = _previousPosition;
        }

        public void SetMovement(Transform position)
        {
            positionToGo = position;
            UpdateAIMode(AIMode.Defending);
        }

        public override Vector3 GetMovement()
        {
            return positionToGo.position;
        }
    }
}