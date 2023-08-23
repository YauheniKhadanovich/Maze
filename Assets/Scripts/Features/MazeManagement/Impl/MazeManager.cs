using System;
using System.Collections;
using Features.CameraManagement;
using Features.Coin.Impl;
using Features.Diamond.Impl;
using Features.Player.Impl;
using Modules.GameController.Facade;
using Modules.MazeGenerator.Data;
using Modules.MazeGenerator.Facade;
using UnityEngine;
using Zenject;

namespace Features.MazeManagement.Impl
{
    public class MazeManager : MonoBehaviour, IMazeManager, IInitializable, IDisposable
    {
        public event Action LevelCleared = delegate { };

        [Inject] 
        private readonly IGameControllerFacade _gameControllerFacade;
        [Inject] 
        private readonly IMazeGenerationFacade _mazeGenerationFacade;
        [Inject] 
        private readonly ICameraManager _cameraManager;
        
        public MazeData MazeData => _mazeGenerationFacade.MazeData;
        
        
        [SerializeField] 
        private DiamondItem _diamondFrefab;
        [SerializeField] 
        private CoinItem _coinFrefab;
        [SerializeField] 
        private GameObject _floorFrefab;
        [SerializeField] 
        private MazePlayer _playerPrefab;
        [SerializeField] 
        private Transform _root;

        private MazePlayer _player;
        private int _diamonds = 0; 

        public void Initialize()
        {
            _gameControllerFacade.GameStartRequested += OnGameStartRequested;
            _gameControllerFacade.GameStopRequested += OnGameStopRequested;
        }

        public void Dispose()
        {
            _gameControllerFacade.GameStartRequested -= OnGameStartRequested;
            _gameControllerFacade.GameStopRequested -= OnGameStopRequested;
        }
        
        private void OnGameStartRequested()
        {
            LevelCleared += Build;
            StartCoroutine(nameof(ClearLevelCoroutine));
        }
        
        private void OnGameStopRequested()
        {
            _player.Disable();
        }
        
        private IEnumerator ClearLevelCoroutine()
        {
            if (_root.childCount > 0)
            {
                Destroy(_root.GetChild(0).gameObject);
                yield return null;
            }

            LevelCleared.Invoke();
        }

        private void Build()
        {
            LevelCleared -= Build;
            Build(_mazeGenerationFacade.MazeData);
            _cameraManager.PlayerCameraSetEnable(true);
            _cameraManager.NoPlayCameraSetEnable(false);
            _cameraManager.SetCameraTarget(_player.transform);
            _player.Enable();
        }

        private void Build(MazeData mazeData)
        {
            var lvlRoot = new GameObject("lvlRoot")
            {
                transform =
                {
                    position = Vector3.zero
                }
            };
            lvlRoot.transform.SetParent(_root);
            
            for (var x = 0; x < mazeData.Field.GetLongLength(0); x++)
            {
                for (var y = 0; y < mazeData.Field.GetLongLength(1); y++)
                {
                    var v = mazeData.Field[x, y];
                    switch (v.Type)
                    {
                        case CellType.Wall:
                            break;
                        case CellType.Start:
                            SpawnPlayer(v.Position);
                            SpawnFloor(new Vector3(v.Position.x, 0, v.Position.y));
                            break;
                        case CellType.Diamond:
                            Instantiate(_diamondFrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity, lvlRoot.transform);
                            SpawnFloor(new Vector3(v.Position.x, 0, v.Position.y));
                            break;
                        default:
                            Instantiate(_coinFrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity, lvlRoot.transform);
                            SpawnFloor(new Vector3(v.Position.x, 0, v.Position.y));
                            break;
                    }
                }
            }
            
            void SpawnPlayer(Vector2Int position)
            {
                if (_player)
                {
                    Destroy(_player);
                }
                _player = Instantiate(_playerPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity, lvlRoot.transform);
                _player.SetData(this, position);
                _player.DiamondTaken += OnDiamondTaken;
                _player.PlayerDestroyed += OnPlayerDestroyed;
            }

            void SpawnFloor(Vector3 position)
            {
                Instantiate(_floorFrefab, position, Quaternion.identity, lvlRoot.transform);
            }
        }

        private void OnDiamondTaken()
        {
            _diamonds++;
            _gameControllerFacade.StopCurrentGame();
        }
        
        private void OnPlayerDestroyed()
        {
            _player.DiamondTaken -= OnDiamondTaken;
            _player.PlayerDestroyed -= OnPlayerDestroyed;
        }
    }
}