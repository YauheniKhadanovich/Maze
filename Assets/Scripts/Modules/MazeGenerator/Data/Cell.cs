using UnityEngine;

namespace Modules.MazeGenerator.Data
{
    public struct Cell
    {
        public Vector2Int Position { get; }

        public CellState State { get; private set; }
        
        public Directions PreviousDirection { get; private set; }

        public Cell(Vector2Int position, CellState state)
        {
            Position = position;
            State = state;
            PreviousDirection = Directions.Left;
        }

        public void SetState(CellState state)
        {
            State = state;
        }

        public void SetPreviousDirection(Directions previousDirection)
        {
            PreviousDirection = previousDirection;
        }
    }
}