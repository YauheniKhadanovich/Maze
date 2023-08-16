using Features.Player.Input.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player.Input
{
    public class PlayerInput : BasePlayerInput
    {
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
        {
            CheckDirection(context.ReadValue<Vector2>());
        }
    }
}