using Modules.MazeGenerator.Data;

namespace Modules.MazeGenerator.Facade
{
    public interface IMazeGenerationFacade
    {
        public void GenerateMaze();

        MazeData MazeData { get; }
    }
}