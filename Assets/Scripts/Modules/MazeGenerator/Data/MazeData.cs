using System;
using System.Collections.Generic;
using UnityEngine;

namespace Modules.MazeGenerator.Data
{
    public class MazeData
    {
        public Cell[,] Field { get; }

        public Cell GetCell(Vector2Int position)
        {
            TryGetCell(position, out var cell);
            return cell;
        }

        public void SetCell(Vector2Int position, CellState state = CellState.Wall)
        {
            Field[position.x, position.y] = new Cell(position, state);
        }

        public Cell MakeWay(Vector2Int from, Directions direction)
        {
            TryGetCell(from, out var cell);
            cell.SetState(CellState.Floor);
            Field[cell.Position.x, cell.Position.y] = cell;
            
            TryGetCell(from, direction, out var nextCell);
            nextCell.SetState(CellState.Floor);
            nextCell.SetPreviousDirection(direction);
            Field[nextCell.Position.x, nextCell.Position.y] = nextCell;

            TryGetWay(from, direction, out var way);
            way.SetState(CellState.Floor);
            Field[way.Position.x, way.Position.y] = way;

            return nextCell;
        }

        public MazeData(int xLength, int yLength)
        {
            Field = new Cell[xLength, yLength];

            for (var x = 0; x < xLength; x++)
            {
                for (var y = 0; y < xLength; y++)
                {
                    SetCell(new Vector2Int(x, y));
                }
            }
        }
        
        public bool TryGetUndefinedDirections(Cell cell, out List<Directions> undefinedDirections)
        {
            undefinedDirections = new List<Directions>();

            if (TryGetCell(cell.Position, Directions.Forward, out var neighborAbove))
            {
                if (neighborAbove.State == CellState.Wall)
                {
                    undefinedDirections.Add(Directions.Forward);
                }
            }

            if (TryGetCell(cell.Position, Directions.Back, out var neighborBelow))
            {
                if (neighborBelow.State == CellState.Wall)
                {
                    undefinedDirections.Add(Directions.Back);
                }
            }

            if (TryGetCell(cell.Position, Directions.Left, out var neighborLeft))
            {
                if (neighborLeft.State == CellState.Wall)
                {
                    undefinedDirections.Add(Directions.Left);
                }
            }

            if (TryGetCell(cell.Position, Directions.Right, out var neighborRight))
            {
                if (neighborRight.State == CellState.Wall)
                {
                    undefinedDirections.Add(Directions.Right);
                }
            }

            return undefinedDirections.Count > 0;
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
        
        private bool TryGetWay(Vector2Int pos, Directions direction, out Cell cell)
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
    }
}