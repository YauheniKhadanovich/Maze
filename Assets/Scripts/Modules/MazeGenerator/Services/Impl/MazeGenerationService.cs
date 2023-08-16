using System.Collections.Generic;
using Modules.MazeGenerator.Data;
using UnityEngine;

namespace Modules.MazeGenerator.Services.Impl
{
    public class MazeGenerationService : IMazeGenerationService
    {
        private readonly Vector2Int _startGenerationPosition = new(1, 1);

        public MazeData MazeData { get; } = new(23, 23);

        public void GenerateMaze()
        {
            var allFrontiers = new List<Vector2Int>();
            allFrontiers.Add(MazeData.GetCell(_startGenerationPosition).Position);
            while (allFrontiers.Count > 0)
            {
                var currentCell = MazeData.GetCell(allFrontiers[Random.Range(0, allFrontiers.Count)]);

                if (MazeData.TryGetUndefinedDirections(currentCell, out var undefinedDirections))
                {
                    if (undefinedDirections.Count > 1 && undefinedDirections.Contains(currentCell.PreviousDirection))
                    {
                        undefinedDirections.Remove(currentCell.PreviousDirection);
                    }

                    var direction = undefinedDirections[Random.Range(0, undefinedDirections.Count)];
                    var nextCell = MazeData.MakeWay(currentCell.Position, direction);
                    if (!allFrontiers.Contains(nextCell.Position))
                    {
                        allFrontiers.Add(nextCell.Position);
                    }
                }

                if (!MazeData.TryGetUndefinedDirections(currentCell, out _))
                {
                    allFrontiers.Remove(currentCell.Position);
                }
            }
            MazeData.SetPlayerPosition(_startGenerationPosition);
        }
    }
}