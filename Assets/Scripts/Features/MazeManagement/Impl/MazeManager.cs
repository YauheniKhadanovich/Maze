using System;
using System.Collections;
using System.Collections.Generic;
using Features.Coin.Impl;
using Features.Diamond.Impl;
using Features.Floor.Impl;
using Features.Player;
using Features.Player.Impl;
using Modules.Core;
using Modules.GameController.Facade;
using Modules.MazeGenerator.Data;
using Modules.MazeGenerator.Facade;
using UnityEngine;
using Zenject;

namespace Features.MazeManagement.Impl
{
    public class MazeManager : MonoBehaviour, IMazeManager, IInitializable, IDisposable
    {
        private event Action LevelCleared = delegate { };

        [Inject]
        private readonly ILevelTimer _levelTimer;
        [Inject]
        private readonly DiContainer _container;
        [Inject] 
        private readonly IGameControllerFacade _gameControllerFacade;
        [Inject] 
        private readonly IMazeGenerationFacade _mazeGenerationFacade;

        public MazeData MazeData => _mazeGenerationFacade.MazeData;
        
        [SerializeField] 
        private DiamondItem _diamondFrefab;
        [SerializeField] 
        private CoinItem _coinFrefab;
        [SerializeField] 
        private FloorItem _floorFrefab;
        [SerializeField] 
        private MazePlayer _playerPrefab;
        [SerializeField] 
        private Transform _root;

        private IMazePlayer _player;
        private Dictionary<Vector2Int, FloorItem> _floorItems = new();

        public void Initialize()
        {
            _gameControllerFacade.LevelBuildRequested += OnLevelBuildRequested;
            _gameControllerFacade.LevelDone += OnLevelDone;
            _gameControllerFacade.GameStarted += OnGameStarted;
            _levelTimer.TimeOut += OnTimeOut;
            _levelTimer.TimeTick += OnTimeTick;
        }
        
        public void Dispose()
        {
            _gameControllerFacade.LevelBuildRequested -= OnLevelBuildRequested;
            _gameControllerFacade.LevelDone -= OnLevelDone;
            _gameControllerFacade.GameStarted -= OnGameStarted;
            _levelTimer.TimeOut -= OnTimeOut;
            _levelTimer.TimeTick -= OnTimeTick;
        }

        private void OnGameStarted(int level)
        {
            _levelTimer.StartTimer(_mazeGenerationFacade.MazeData.TimeForMaze);
        }

        private void OnLevelBuildRequested()
        {
            LevelCleared += BuildAndStart;
            StartCoroutine(nameof(ClearLevelCoroutine));
        }
        
        private void OnLevelDone(LevelResult result)
        {
            _levelTimer.StopTimer();
        }
        
        private IEnumerator ClearLevelCoroutine()
        {
            if (_root.childCount > 0)
            {
                Destroy(_root.GetChild(0).gameObject);
                _floorItems.Clear();
                yield return null;
            }

            LevelCleared.Invoke();
        }

        private void BuildAndStart()
        {
            LevelCleared -= BuildAndStart;
            Build(_mazeGenerationFacade.MazeData);
            _gameControllerFacade.ReportGameStarted(_mazeGenerationFacade.MazeData.DiamondCount,_mazeGenerationFacade.MazeData.TimeForMaze);
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
                            SpawnFloor(v.Position);
                            break;
                        case CellType.Diamond:
                            Instantiate(_diamondFrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity, lvlRoot.transform);
                            SpawnFloor(v.Position);
                            break;
                        default:
                            Instantiate(_coinFrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity, lvlRoot.transform);
                            SpawnFloor(v.Position);
                            break;
                    }
                }
            }
            
            void SpawnPlayer(Vector2Int position)
            {
                if (_player != null)
                {
                    if (_player is IDisposable disposablePlayer)
                    {
                        disposablePlayer.Dispose();
                    }
                    if ((MonoBehaviour)_player)
                    {
                        Destroy(((MonoBehaviour)_player).gameObject);
                    }
                }
                _player = _container.InstantiatePrefabForComponent<MazePlayer>(_playerPrefab, new Vector3(position.x, 0, position.y), Quaternion.identity, lvlRoot.transform);
              
                if (_player is IInitializable initializablePlayer)
                {
                    initializablePlayer.Initialize();
                }
                
                _player.SetData(position);
            }

            void SpawnFloor(Vector2Int mazePos)
            {
                var floorItem = Instantiate(_floorFrefab, new Vector3(mazePos.x, 0, mazePos.y), Quaternion.identity, lvlRoot.transform);
                _floorItems.Add(mazePos, floorItem);
            }
        }
        
        public void ReportPlayerFailed()
        {
            _gameControllerFacade.ReportPlayerFailed();
        }
        
        private void OnTimeTick(int timeInSeconds)
        {
            _gameControllerFacade.ReportTimerTick(timeInSeconds);
        }
        
        private void OnTimeOut()
        {
            _gameControllerFacade.ReportOutOfTime();
        }
    }
}