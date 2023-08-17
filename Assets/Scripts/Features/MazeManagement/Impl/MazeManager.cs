using System;
using Features.CameraManagement;
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
        [Inject] 
        private readonly IGameControllerFacade _gameControllerFacade;
        [Inject] 
        private readonly IMazeGenerationFacade _mazeGenerationFacade;
        [Inject] 
        private readonly ICameraManager _cameraManager;

        [SerializeField] 
        private GameObject _wallPrefab;
        [SerializeField] 
        private GameObject _diamondFrefab;
        [SerializeField] 
        private GameObject _floorFrefab;
        [SerializeField] 
        private MazePlayer _playerPrefab;

        private MazePlayer _player;

        public void Initialize()
        {
            _gameControllerFacade.GameStarted += OnGameStarted;

        }

        public void Dispose()
        {
            _gameControllerFacade.GameStarted -= OnGameStarted;
        }

        private void OnGameStarted()
        {
            Build(_mazeGenerationFacade.MazeData);
            _cameraManager.PlayerCameraSetEnable(true);
            _cameraManager.NoPlayCameraSetEnable(false);
            _cameraManager.SetCameraTarget(_player.transform);
        }
        
        public MazeData GetMazeData()
        {
            return _mazeGenerationFacade.MazeData;
        }

        private void Build(MazeData mazeData)
        {
            for (var x = 0; x < mazeData.Field.GetLongLength(0); x++)
            {
                for (var y = 0; y < mazeData.Field.GetLongLength(1); y++)
                {
                    var v = mazeData.Field[x, y];
                    switch (v.Type)
                    {
                        case CellType.Wall:
                         //   Instantiate(_wallPrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity);
                            break;
                        case CellType.Start:
                            if (_player)
                            {
                                Destroy(_player);
                            }
                            _player = Instantiate(_playerPrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity);
                            _player.SetData(this, v.Position);
                            SpawnFloor(new Vector3(v.Position.x, 0, v.Position.y));
                            break;
                        case CellType.Diamond:
                            Instantiate(_diamondFrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity);
                            SpawnFloor(new Vector3(v.Position.x, 0, v.Position.y));
                            break;
                        default:
                            SpawnFloor(new Vector3(v.Position.x, 0, v.Position.y));
                            break;
                    }
                }
            }

            void SpawnFloor(Vector3 position)
            {
                Instantiate(_floorFrefab, position, Quaternion.identity);
            }
        }
    }
}