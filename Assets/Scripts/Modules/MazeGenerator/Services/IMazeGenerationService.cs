using Modules.MazeGenerator.Data;

namespace Modules.MazeGenerator.Services
{
    public interface IMazeGenerationService
    {
        public void GenerateMaze(int xCount, int yCount);

        MazeData MazeData { get; }
    }
}