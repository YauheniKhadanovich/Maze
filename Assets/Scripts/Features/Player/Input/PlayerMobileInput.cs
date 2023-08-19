using Features.Player.Input.Base;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player.Input
{
    public class PlayerMobileInput : BasePlayerInput
    {
        private const float _minDistance = 15f;
        private const float _maxTime = 1f;

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

            if (Vector3.Distance(_startPosition, _endPosition) >= _minDistance && (_endTime - _startTime) < _maxTime)
            {
                Vector3 directionV3 = _endPosition - _startPosition;
                var direction = new Vector2(directionV3.x, directionV3.y).normalized;
                CheckDirection(direction);
            }
        }
    }
}