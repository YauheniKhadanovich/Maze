using Modules.MazeGenerator.Data;

namespace Modules.MazeGenerator.Services.Impl
{
    public class MazeGenerationService : IMazeGenerationService
    {
        public MazeData MazeData { get; private set; }

        public void GenerateMaze(int xCount, int yCount)
        {
            MazeData = new MazeData(xCount, yCount).Generate();
        }
    }
}