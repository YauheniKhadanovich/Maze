using System;
using Modules.MazeGenerator.Facade;
using Zenject;

namespace Modules.GameController.Service.Impl
{
    public class GameControllerService : IGameControllerService, IInitializable
    {
        [Inject] 
        private readonly IMazeGenerationFacade _mazeGeneration;

        public event Action GameStarted = delegate { };
        
        private int _currentLevel = 0;

        public void Initialize()
        {
            _currentLevel = 1;
        }
        
        public void StartNextGame()
        {
            _mazeGeneration.GenerateMaze(3 + 2 * _currentLevel, 3 + 2 * _currentLevel);
            BuildAndStart();
        }

        private void BuildAndStart()
        {
            GameStarted.Invoke();
        }
    }
}