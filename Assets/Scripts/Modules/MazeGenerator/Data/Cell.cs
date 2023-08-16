using UnityEngine;

namespace Modules.MazeGenerator.Data
{
    public struct Cell
    {
        public Vector2Int Position { get; }

        public CellType Type { get; private set; }
        
        public Directions PreviousDirection { get; private set; }

        public Cell(Vector2Int position, CellType type)
        {
            Position = position;
            Type = type;
            PreviousDirection = Directions.Left;
        }

        public void SetState(CellType type)
        {
            Type = type;
        }

        public void SetPreviousDirection(Directions previousDirection)
        {
            PreviousDirection = previousDirection;
        }
    }
}