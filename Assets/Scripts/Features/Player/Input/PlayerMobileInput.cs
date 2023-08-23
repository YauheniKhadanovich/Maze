using Features.Player.Input.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player.Input
{
    public class PlayerMobileInput : BasePlayerInput
    {
        private const float MinDistance = 15f;
        private const float MaxTime = 1f;

        private Vector2 _startPosition;
        private float _startTime;
        private Vector2 _endPosition;
        private float _endTime;
        
        private void Start()
        {
            Controls.MazeMap.PrimaryContact.started += ContactStarted;
            Controls.MazeMap.PrimaryContact.canceled += ContactCanceled;
        }
        
        private void OnDisable()
        {
            DisablePlayer();
        }

        private void ContactStarted(InputAction.CallbackContext context)
        {
            _startPosition = Controls.MazeMap.PrimaryPosition.ReadValue<Vector2>();
            _startTime = (float)context.startTime;
        }

        private void ContactCanceled(InputAction.CallbackContext context)
        {
            _endPosition = Controls.MazeMap.PrimaryPosition.ReadValue<Vector2>();
            _endTime = (float)context.time;

            if (Vector3.Distance(_startPosition, _endPosition) >= MinDistance && (_endTime - _startTime) < MaxTime)
            {
                Vector3 directionV3 = _endPosition - _startPosition;
                var direction = new Vector2(directionV3.x, directionV3.y).normalized;
                CheckDirection(direction);
            }
        }
    }
}