using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player.Impl
{
    public class PlayerInput : BasePlayerInput, IPlayerInput
    {
        public event  Action OnRight = delegate { };
        public event  Action OnLeft = delegate { };
        public event  Action OnForward = delegate { };
        public event  Action OnBack = delegate { };
        
        protected override void Awake()
        {
            base.Awake();
            Controls.MazeMap.Movements.performed += OmMovementsPerformed;
        }

        private void OnEnable()
        {
            Controls.Enable();
        }

        private void OnDisable()
        {
            Controls.Disable();
        }
        
        private void OmMovementsPerformed(InputAction.CallbackContext context)
        {;
            CheckInputValue(context.ReadValue<Vector2>());
         }

        private void CheckInputValue(Vector2 direction)
        {
            if (Vector2.Dot(Vector2.up, direction) > 0.9f)
            {
                OnForward();
            }
            else if (Vector2.Dot(Vector2.down, direction) > 0.9f)
            {
                OnBack();
            }
            else if (Vector2.Dot(Vector2.left, direction) > 0.9f)
            {
                OnLeft();
            }
            else if (Vector2.Dot(Vector2.right, direction) > 0.9f)
            {
                OnRight();
            }
        }
    }
}