using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.MazeGenerator.Data
{
    public class MazeData
    {
        public Cell[,] Field { get; }
        public int DiamondCount { get; private set; }

        private int _XLength = 0;
        private int _YLength = 0;
        
        public MazeData(int xLength, int yLength)
        {
            _XLength = xLength;
            _YLength = yLength;
            Field = new Cell[xLength, yLength];

            for (var x = 0; x < xLength; x++)
            {
                for (var y = 0; y < xLength; y++)
                {
                    SetCell(new Vector2Int(x, y));
                }
            }
        }

        public MazeData Generate()
        {
            var xStartPos = UnityEngine.Random.Range(1, _XLength - 1); 
            xStartPos = xStartPos % 2 == 0 ? xStartPos - 1 : xStartPos;
            
            var yStartPos = UnityEngine.Random.Range(1, _YLength - 1); 
            yStartPos = yStartPos % 2 == 0 ? yStartPos - 1 : yStartPos;
            
            var startGenerationPosition = new Vector2Int(xStartPos, yStartPos);
            var allFrontiers = new List<Vector2Int> { startGenerationPosition };
            
            TryGetCell(startGenerationPosition, out var cell);
            cell.SetState(CellType.Floor);
            Field[cell.Position.x, cell.Position.y] = cell;
            
            while (allFrontiers.Count > 0)
            {
                var currentCell = GetCell(allFrontiers[UnityEngine.Random.Range(0, allFrontiers.Count)]);

                if (TryGetUndefinedDirections(currentCell, out var undefinedDirections))
                {
                    if (undefinedDirections.Count > 1 && undefinedDirections.Contains(currentCell.PreviousDirection))
                    {
                        undefinedDirections.Remove(currentCell.PreviousDirection);
                    }

                    var direction = undefinedDirections[UnityEngine.Random.Range(0, undefinedDirections.Count)];
                    var nextCell = MakeWay(currentCell.Position, direction);
                    if (!allFrontiers.Contains(nextCell.Position))
                    {
                        allFrontiers.Add(nextCell.Position);
                    }
                }

                if (!TryGetUndefinedDirections(currentCell, out _))
                {
                    allFrontiers.Remove(currentCell.Position);
                    if (GetPassesCount(currentCell.Position) == 1)
                    {
                        SetDiamondPosition(currentCell.Position);
                    }
                }
            }

            // Put player start position
            var possiblePlayerStartPositions = new List<Vector2Int>();
            for (var x = 0; x < _XLength; x++)
            {
                for (var y = 0; y < _YLength; y++)
                {
                    if (Field[x, y].Type == CellType.Floor)
                    {
                        possiblePlayerStartPositions.Add(Field[x, y].Position);
                    }
                }              
            }

            SetPlayerPosition(possiblePlayerStartPositions[UnityEngine.Random.Range(0, possiblePlayerStartPositions.Count)]);
            
            return this;
        }

        public Cell GetCell(Vector2Int position)
        {
            TryGetCell(position, out var cell);
            return cell;
        }

        public void DestroyCell(Vector2Int position)
        {
            TryGetCell(position, out var cell);
            cell.DestroyCell();
            Field[cell.Position.x, cell.Position.y] = cell;
        }
        
        private void SetDiamondPosition(Vector2Int position)
        {
            TryGetCell(position, out var cell);
            cell.SetState(CellType.Diamond);
            Field[cell.Position.x, cell.Position.y] = cell;
            DiamondCount++;
        }
        
        private void SetPlayerPosition(Vector2Int position)
        {
            TryGetCell(position, out var cell);
            cell.SetState(CellType.Start);
            Field[cell.Position.x, cell.Position.y] = cell;
        }

        private Cell MakeWay(Vector2Int from, Directions direction)
        {
            TryGetCell(from, direction, out var nextCell);
            nextCell.SetState(CellType.Floor);
            nextCell.SetPreviousDirection(direction);
            Field[nextCell.Position.x, nextCell.Position.y] = nextCell;

            TryGetPass(from, direction, out var way);
            way.SetState(CellType.Floor);
            Field[way.Position.x, way.Position.y] = way;

            return nextCell;
        }
        
        private bool TryGetUndefinedDirections(Cell cell, out List<Directions> undefinedDirections)
        {
            undefinedDirections = new List<Directions>();

            if (TryGetCell(cell.Position, Directions.Forward, out var neighborAbove))
            {
                if (neighborAbove.Type == CellType.Wall)
                {
                    undefinedDirections.Add(Directions.Forward);
                }
            }

            if (TryGetCell(cell.Position, Directions.Back, out var neighborBelow))
            {
                if (neighborBelow.Type == CellType.Wall)
                {
                    undefinedDirections.Add(Directions.Back);
                }
            }

            if (TryGetCell(cell.Position, Directions.Left, out var neighborLeft))
            {
                if (neighborLeft.Type == CellType.Wall)
                {
                    undefinedDirections.Add(Directions.Left);
                }
            }

            if (TryGetCell(cell.Position, Directions.Right, out var neighborRight))
            {
                if (neighborRight.Type == CellType.Wall)
                {
                    undefinedDirections.Add(Directions.Right);
                }
            }

            return undefinedDirections.Count > 0;
        }
        
        private void SetCell(Vector2Int position, CellType type = CellType.Wall)
        {
            Field[position.x, position.y] = new Cell(position, type);
        }

        private bool TryGetCell(Vector2Int position, out Cell cell)
        {
            try
            {
                cell = Field[position.x, position.y];
            }
            catch (Exception)
            {
                cell = new Cell();
                return false;
            }

            return true;
        }
        
        private bool TryGetPass(Vector2Int pos, Directions direction, out Cell cell)
        {
            cell = new Cell();
            try
            {
                switch (direction)
                {
                    case (Directions.Forward):
                        cell = Field[pos.x, pos.y + 1];
                        break;
                    case (Directions.Back):
                        cell = Field[pos.x, pos.y - 1];
                        break;
                    case (Directions.Left):
                        cell = Field[pos.x - 1, pos.y];
                        break;
                    case (Directions.Right):
                        cell = Field[pos.x + 1, pos.y];
                        break;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool TryGetCell(Vector2Int pos, Directions direction, out Cell cell)
        {
            cell = new Cell();
            try
            {
                switch (direction)
                {
                    case (Directions.Forward):
                        cell = Field[pos.x, pos.y + 2];
                        break;
                    case (Directions.Back):
                        cell = Field[pos.x, pos.y - 2];
                        break;
                    case (Directions.Left):
                        cell = Field[pos.x - 2, pos.y];
                        break;
                    case (Directions.Right):
                        cell = Field[pos.x + 2, pos.y];
                        break;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private int GetPassesCount(Vector2Int pos)
        {
            var count = 0;
            if (TryGetPass(pos, Directions.Forward, out var p) && p.Type != CellType.Wall)
            {
                count++;
            }

            if (TryGetPass(pos, Directions.Back, out p) && p.Type != CellType.Wall)
            {
                count++;
            }

            if (TryGetPass(pos, Directions.Left, out p) && p.Type != CellType.Wall)
            {
                count++;
            }

            if (TryGetPass(pos, Directions.Right, out p) && p.Type != CellType.Wall)
            {
                count++;
            }

            return count;
        }
    }
}