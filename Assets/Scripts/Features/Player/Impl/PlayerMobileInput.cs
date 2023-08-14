using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Features.Player.Impl
{
    public class PlayerMobileInput : BasePlayerInput, IPlayerInput
    {
        public event Action OnRight = delegate { };
        public event Action OnLeft = delegate { };
        public event Action OnForward = delegate { };
        public event Action OnBack = delegate { };

        private const float _minDistance = 15f;
        private const float _maxTime = 1f;
        private const float _threshold = 0.8f;

        private Vector2 _startPosition;
        private float _startTime;
        private Vector2 _endPosition;
        private float _endTime;


        private void Start()
        {
            Controls.MazeMap.PrimaryContact.started += ContactStarted;
            Controls.MazeMap.PrimaryContact.canceled += ContactCanceled;
        }

        private void OnEnable()
        {
            Controls.Enable();
        }

        private void OnDisable()
        {
            Controls.Disable();
        }

        private void ContactStarted(InputAction.CallbackContext context)
        {
            _startPosition = Controls.MazeMap.PrimaryPosition.ReadValue<Vector2>();
            _startTime =  (float)context.startTime;
            Debug.Log("ContactStarted" + _startPosition + " start time  " + _startTime);
        }

        private void ContactCanceled(InputAction.CallbackContext context)
        {
            _endPosition = Controls.MazeMap.PrimaryPosition.ReadValue<Vector2>();
            _endTime = (float)context.time;
            Debug.Log("ContactCanceled" + _endPosition + " end time  " + _endTime);
            CheckInputValue();
        }

        private void CheckInputValue()
        {
            if (Vector3.Distance(_startPosition, _endPosition) >= _minDistance && (_endTime - _startTime) < _maxTime)
            {
                Vector3 directionV3 = _endPosition - _startPosition;
                Vector2 direction = new Vector2(directionV3.x, directionV3.y).normalized;
                if (Vector2.Dot(Vector2.up, direction) > _threshold)
                {
                    OnForward();
                }
                else if (Vector2.Dot(Vector2.down, direction) > _threshold)
                {
                    OnBack();
                }
                else if (Vector2.Dot(Vector2.left, direction) > _threshold)
                {
                    OnLeft();
                }
                else if (Vector2.Dot(Vector2.right, direction) > _threshold)
                {
                    OnRight();
                }
            }
        }
    }
}