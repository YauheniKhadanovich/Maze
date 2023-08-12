using System;
using Modules.MazeGenerator.Data;
using Modules.MazeGenerator.Services.Impl;
using UnityEngine;

namespace Features.MazeBuilder
{
    public class MazeBuilderManager : MonoBehaviour
    {
        private MazeGenerationService _mazeGenerationService = new MazeGenerationService();
        [SerializeField] 
        private GameObject _wallPrefab;
        [SerializeField] 
        private GameObject _stonePrefab;
        
        private void Start()
        {
            _mazeGenerationService.GenerateMaze();
            for (var x = 0; x < _mazeGenerationService.MazeData.Field.GetLongLength(0); x++)
            {
                for (var y = 0; y < _mazeGenerationService.MazeData.Field.GetLongLength(1); y++)
                {
                    var v = _mazeGenerationService.MazeData.Field[x, y];
                    if (v.State == CellState.Wall)
                    {
                        var g = GameObject.Instantiate(_wallPrefab, new Vector3(v.Position.x, 0, v.Position.y), Quaternion.identity);
                    }
                }
            }
        }
    }
}