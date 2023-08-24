using Features.MazeManagement.Impl;
using Modules.MazeGenerator.Data;
using UnityEngine;

namespace Features.Player.Impl
{
    public class MazePlayer : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem _diamondParticlePrefab;
        [SerializeField] 
        private ParticleSystem _failParticlePrefab;
        
        private IPlayerInput _playerInput;
        private Vector2Int _mazePosition;
        private MazeManager _mazeManager;

        private bool _isMovementInProgress;

        private void Awake()
        {
            _playerInput = GetComponent<IPlayerInput>();
            _playerInput.OnBack += MoveBack;
            _playerInput.OnLeft += MoveLeft;
            _playerInput.OnForward += MoveForward;
            _playerInput.OnRight += MoveRight;
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
            if (!_isMovementInProgress)
            {
                return;
            }
            
            var destination = new Vector3(_mazePosition.x, 0, _mazePosition.y);
            transform.position = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * 17f);

            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                _isMovementInProgress = false;
                if (_mazeManager.MazeData.GetCell(_mazePosition).Type == CellType.Wall)
                {
                    var failPS = Instantiate(_failParticlePrefab, transform.position, Quaternion.LookRotation(Vector3.up), null);
                    failPS.Play();
                    _mazeManager.PlayerFailed();
                    Destroy(gameObject);
                }
            }
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
            if (_isMovementInProgress)
            {
                return;
            }
            
            var nextCell = _mazeManager.MazeData.GetCell(_mazePosition + direction);
            if (_mazePosition != nextCell.Position)
            {
                _isMovementInProgress = true;
                _mazePosition = nextCell.Position;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Diamond"))
            {
                var diamondPS = Instantiate(_diamondParticlePrefab, other.transform.position, Quaternion.LookRotation(Vector3.up), null);
                diamondPS.Play();
                Destroy(other.gameObject);
                _mazeManager.OnDiamondTaken();
            }
            
            if (other.gameObject.CompareTag("Coin"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}