using System;
using Features.CameraManagement;
using Features.MazeManagement;
using Modules.Core;
using Modules.GameController.Facade;
using Modules.MazeGenerator.Data;
using UnityEngine;
using Zenject;

namespace Features.Player.Impl
{
    public class MazePlayer : MonoBehaviour, IMazePlayer, IInitializable, IDisposable
    {
        [Inject] 
        private readonly IMazeManager _mazeManager;
        [Inject] 
        private readonly IGameControllerFacade _gameControllerFacade;
        [Inject] 
        private ICameraManager _cameraManager;
        
        [SerializeField] 
        private ParticleSystem _diamondParticlePrefab;
        [SerializeField] 
        private ParticleSystem _failParticlePrefab;
        
        private IPlayerInput _playerInput;
        private Vector2Int _mazePosition;
        private bool _isMovementInProgress;

        public void Initialize()
        {
            _gameControllerFacade.GameStarted += OnGameStarted;
            _gameControllerFacade.LevelDone += OnLevelDone;
        }
        
        public void Dispose()
        {
            _gameControllerFacade.LevelDone -= OnLevelDone;
            _gameControllerFacade.GameStarted -= OnGameStarted;
        }

        private void Awake()
        {
            _playerInput = GetComponent<IPlayerInput>();
            _playerInput.OnBack += MoveBack;
            _playerInput.OnLeft += MoveLeft;
            _playerInput.OnForward += MoveForward;
            _playerInput.OnRight += MoveRight;
        }
        
        private void OnGameStarted()
        {
            _cameraManager.PlayerCameraSetEnable(true);
            _cameraManager.NoPlayCameraSetEnable(false);
            _cameraManager.SetCameraTarget(transform);
            EnableInput();
        }

        private void OnLevelDone(LevelResult obj)
        {
            DisableInput();
            DestroyPlayer();
        }

        public void SetData(Vector2Int startPosition)
        {
            _mazePosition = startPosition;
        }

        private void EnableInput()
        {
            _playerInput.EnablePlayer();
        }

        private void DisableInput()
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
                    _mazeManager.ReportPlayerFailed();
                    DestroyPlayer();
                }
            }
        }

        private void DestroyPlayer()
        {
            var failPS = Instantiate(_failParticlePrefab, transform.position, Quaternion.LookRotation(Vector3.up), null);
            failPS.Play();
            Destroy(gameObject);
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
                _gameControllerFacade.ReportDiamondTaken();
            }
            
            if (other.gameObject.CompareTag("Coin"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}