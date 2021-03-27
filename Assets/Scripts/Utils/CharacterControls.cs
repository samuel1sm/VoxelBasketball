using System;
using UnityEngine;

namespace Utils
{
    public enum ButtonInputTypes{
        Started, Performed, Canceled
    }
    
    

    public abstract class CharacterControls : MonoBehaviour
    {
        [SerializeField] protected bool teamOne { get; }
        public event Action<ButtonInputTypes> FirstActionPressed;
        public event Action<ButtonInputTypes> SecondActionPressed;

        public abstract void MovementActivation(ButtonInputTypes types);

        protected virtual void OnFirstActionPressed(ButtonInputTypes obj)
        {
            FirstActionPressed?.Invoke(obj);
        }
        
        public abstract Vector3 GetMovement();

        protected virtual void OnSecondActionPressed(ButtonInputTypes obj)
        {
            SecondActionPressed?.Invoke(obj);
        }
    }
    
  
}
