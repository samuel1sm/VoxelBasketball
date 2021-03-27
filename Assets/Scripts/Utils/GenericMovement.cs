using System;
using UnityEngine;
using UnityEngine.Serialization;


namespace Utils
{
    [RequireComponent(typeof(CharacterControls))]
    public abstract class GenericMovement : MonoBehaviour
    {
        [SerializeField] protected float stamina2Lose = 1f;
        [SerializeField] protected float characterSpeed;
        [SerializeField] protected float turnSmoothTime;
        protected CharacterStatus characterStatus;

        protected virtual void LoseStamina()
        {
            characterStatus.UpdateStamina(-stamina2Lose);
        }
        
        protected abstract void Move(Vector3 position);
    }
}