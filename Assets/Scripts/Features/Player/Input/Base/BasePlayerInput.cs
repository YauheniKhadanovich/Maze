using System;
using UnityEngine;

namespace Features.Player.Input.Base
{
    public abstract class BasePlayerInput : MonoBehaviour, IPlayerInput
    {
        protected MazeControls Controls;
        
        private const float Threshold = 0.8f;
        public event Action OnRight = delegate { };
        public event Action OnLeft = delegate { };
        public event Action OnForward = delegate { };
        public event Action OnBack = delegate { };
        
        protected virtual void Awake()
        {
            Controls = new MazeControls();
        }
        
        protected void CheckDirection(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > Threshold)
            {
                OnForward();
            }
            else if (Vector2.Dot(Vector2.down, direction) > Threshold)
            {
                OnBack();
            }
            else if (Vector2.Dot(Vector2.left, direction) > Threshold)
            {
                OnLeft();
            }
            else if (Vector2.Dot(Vector2.right, direction) > Threshold)
            {
                OnRight();
            }
        }
    }
}