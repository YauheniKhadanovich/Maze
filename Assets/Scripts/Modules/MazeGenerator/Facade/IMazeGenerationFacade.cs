using Modules.MazeGenerator.Data;

namespace Modules.MazeGenerator.Facade
{
    public interface IMazeGenerationFacade
    {
        public void GenerateMaze(int xCount, int yCount);

        MazeData MazeData { get; }
    }
}