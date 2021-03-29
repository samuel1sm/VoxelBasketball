using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using Utils;
using Random = UnityEngine.Random;

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
        [SerializeField] private float actionDelay;
        [SerializeField] private float shootRange = 6;

        private Transform _previousPosition;
        private CharacterStatus _characterStatus;
        private AIMode _aiMode = AIMode.Chasing;
        private NavMeshAgent _navMesh;
        private bool _shooted = false;
        public event Action<AIMode> OnModeUpdated = delegate(AIMode b) { };

        private void Awake()
        {
            _characterStatus = GetComponent<CharacterStatus>();
            _navMesh = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            // _characterStatus.OnBallStollen += () => UpdateAIMode(AIMode.Defending);
            // _characterStatus.OnCatchTheBall += _ => UpdateAIMode(AIMode.Advancing);
            // _ballManager.OnStateUpdated += HandleBallUpdate;

            StartCoroutine(AIActionsDecider());
        }

        public void UpdateAIMode(AIMode aiMode)
        {
            _aiMode = aiMode;
            OnModeUpdated(_aiMode);
        }
        

        private void Update()
        {
          
        }

        private void HandleDefence()
        {
            // print($"{positionToGo.position} {positionToGo.gameObject.name}");
            if (DistanceToTarget() <=
                _navMesh.stoppingDistance)
            {
                // transform.LookAt(positionToGo.position);
                transform.DOLookAt(positionToGo.position, 0.2F, AxisConstraint.Y);
                OnSecondActionPressed(ButtonInputTypes.Started);
            }
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
        }

        public override Vector3 GetMovement()
        {
            return positionToGo.position;
        }


        IEnumerator AIActionsDecider()
        {
            while (true)
            {
                switch (_aiMode)
                {
                    case AIMode.Advancing:
                        HandleAttack();
                        break;
                    case AIMode.Defending:
                        yield return new WaitForSeconds(actionDelay);
                        HandleDefence();
                        break;
                    case AIMode.Chasing:
                        break;
                    case AIMode.Lurking:
                        break;
                    case AIMode.Escaping:
                        break;
                }

                yield return new WaitForSeconds(0f);
            }
        }

        private float DistanceToTarget()
        {
            return Vector3.Distance(positionToGo.position, transform.position);
        }

        private void HandleAttack()
        {
            if (shootRange >= DistanceToTarget() && !_shooted)
            {
                _shooted = true;
                OnFirstActionPressed(ButtonInputTypes.Started);
                StartCoroutine(ShootRelease());
            }
        }

        IEnumerator ShootRelease()
        {

            var value = Random.Range(0f, 0.5f);
            yield return new WaitForSeconds(value);
            OnFirstActionPressed(ButtonInputTypes.Canceled);
            UpdateAIMode(AIMode.Defending);
            // SetMovement(BallManager.Instance.transform);
            _shooted = false;
        }
    }
}