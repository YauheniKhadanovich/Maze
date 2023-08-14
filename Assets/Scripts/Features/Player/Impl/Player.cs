using System;
using UnityEngine;

namespace Features.Player
{
    public class Player: MonoBehaviour
    {
        private IPlayerInput _playerInput;
        private Vector3 _nextPosition;

        private void Awake()
        {
            _playerInput = GetComponent<IPlayerInput>();
            _playerInput.OnBack += MoveBack;
            _playerInput.OnLeft += MoveLeft;
            _playerInput.OnForward += MoveForward;
            _playerInput.OnRight += MoveRight;
            _nextPosition = transform.position;
        }
        
        private void Update()
        {
            transform.position = _nextPosition;
        }
        
        private void MoveForward()
        {
            ProceedMovement(Vector3.forward);
        }
        
        private void MoveBack()
        {
            ProceedMovement(Vector3.back);
        }
        
        private void MoveRight()
        {
            ProceedMovement(Vector3.right);
        }

        private void MoveLeft()
        {
            ProceedMovement(Vector3.left);
        }

        private void ProceedMovement(Vector3 direction)
        {
            _nextPosition = transform.position + direction;
        }
    }
}