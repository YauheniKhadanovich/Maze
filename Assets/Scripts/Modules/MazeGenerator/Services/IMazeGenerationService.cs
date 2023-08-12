using Modules.MazeGenerator.Data;

namespace Modules.MazeGenerator.Services
{
    public interface IMazeGenerationService
    {
        public void GenerateMaze();

        MazeData MazeData { get; }
    }
}