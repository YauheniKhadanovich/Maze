using System;
using Features.MazeManagement.Impl;
using Modules.MazeGenerator.Data;
using UnityEngine;

namespace Features.Player.Impl
{
    public class MazePlayer : MonoBehaviour
    {
        public event Action DiamondTaken = delegate { };
        public event Action PlayerDestroyed = delegate { };
        
        private IPlayerInput _playerInput;
        private Vector2Int _mazePosition;
        private MazeManager _mazeManager;

        private void Awake()
        {
            _playerInput = GetComponent<IPlayerInput>();
            _playerInput.OnBack += MoveBack;
            _playerInput.OnLeft += MoveLeft;
            _playerInput.OnForward += MoveForward;
            _playerInput.OnRight += MoveRight;
        }

        private void OnDestroy()
        {
            PlayerDestroyed.Invoke();
        }

        public void SetData(MazeManager mazeManager, Vector2Int startPosition)
        {
            _mazeManager = mazeManager;
            _mazePosition = startPosition;
        }

        public void Enable()
        {
            _playerInput.EnablePlayer();
        }
        
        public void Disable()
        {
            _playerInput.DisablePlayer();
        }

        private void Update()
        {
            transform.position = new Vector3(_mazePosition.x,0, _mazePosition.y);
        }

        private void MoveForward()
        {
            ProceedMovement(Vector2Int.up);
        }

        private void MoveBack()
        {
            ProceedMovement(Vector2Int.down);
        }

        private void MoveRight()
        {
            ProceedMovement(Vector2Int.right);
        }

        private void MoveLeft()
        {
            ProceedMovement(Vector2Int.left);
        }

        private void ProceedMovement(Vector2Int direction)
        {
            var nextCell = _mazeManager.MazeData.GetCell(_mazePosition + direction);
            if (nextCell.Type != CellType.Wall)
            {
                _mazePosition = nextCell.Position;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Diamond"))
            {
                Destroy(other.gameObject);
                DiamondTaken.Invoke();
            }
        }
    }
}