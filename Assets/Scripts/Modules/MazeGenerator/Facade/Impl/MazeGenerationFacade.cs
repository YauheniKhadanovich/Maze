using Modules.MazeGenerator.Data;
using Modules.MazeGenerator.Services;
using Zenject;

namespace Modules.MazeGenerator.Facade.Impl
{
    public class MazeGenerationFacade : IMazeGenerationFacade
    {
        [Inject] 
        private IMazeGenerationService _mazeGenerationService;

        public MazeData MazeData => _mazeGenerationService.MazeData;

        public void GenerateMaze()
        {
            _mazeGenerationService.GenerateMaze();
        }
    }
}