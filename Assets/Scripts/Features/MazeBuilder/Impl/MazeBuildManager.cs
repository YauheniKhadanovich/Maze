using System;
using Modules.MazeGenerator.Data;
using Modules.MazeGenerator.Facade;
using UnityEngine;
using Zenject;

namespace Features.MazeBuilder.Impl
{
    public class MazeBuildManager : MonoBehaviour, IMazeBuildManager, IInitializable, IDisposable
    {
        [Inject] 
        private IMazeGenerationFacade _mazeGenerationFacade;

        [SerializeField] 
        private GameObject _wallPrefab;

        public void Initialize()
        {
        }

        public void Dispose()
        {
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
                    if (v.State == CellState.Wall)
                    {
                        Instantiate(_wallPrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity);
                    }
                }
            }
        }
    }
}