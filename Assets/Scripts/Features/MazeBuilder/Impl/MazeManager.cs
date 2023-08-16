using System;
using Features.Player.Impl;
using Modules.MazeGenerator.Data;
using Modules.MazeGenerator.Facade;
using UnityEngine;
using Zenject;

namespace Features.MazeBuilder.Impl
{
    public class MazeManager : MonoBehaviour, IMazeManager, IInitializable, IDisposable
    {
        [Inject] 
        private IMazeGenerationFacade _mazeGenerationFacade;

        [SerializeField] 
        private GameObject _wallPrefab;
        [SerializeField] 
        private GameObject _diamondFrefab;
        [SerializeField] 
        private GameObject _floorFrefab;
        [SerializeField] 
        private MazePlayer _playerPrefab;

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }

        public MazeData GetMazeData()
        {
            return _mazeGenerationFacade.MazeData;
        }

        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            _mazeGenerationFacade.GenerateMaze();
            for (var x = 0; x < _mazeGenerationFacade.MazeData.Field.GetLongLength(0); x++)
            {
                for (var y = 0; y < _mazeGenerationFacade.MazeData.Field.GetLongLength(1); y++)
                {
                    var v = _mazeGenerationFacade.MazeData.Field[x, y];
                    switch (v.Type)
                    {
                        case CellType.Wall:
                            Instantiate(_wallPrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity);
                            break;
                        case CellType.Start:
                            var player = Instantiate(_playerPrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity);
                            player.SetData(this, v.Position);
                            SpawnFloor(new Vector3(v.Position.x, 0, v.Position.y));
                            break;
                        case CellType.Diamond:
                            Instantiate(_diamondFrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity);
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