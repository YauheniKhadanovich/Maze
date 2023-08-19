using System;
using Modules.MazeGenerator.Facade;
using Zenject;

namespace Modules.GameController.Service.Impl
{
    public class GameControllerService : IGameControllerService, IInitializable
    {
        [Inject] 
        private readonly IMazeGenerationFacade _mazeGeneration;

        public event Action GameStartRequested = delegate { };
        public event Action GameStopRequested = delegate { };

        private int _currentLevel;
        
        public void Initialize()
        {
            _currentLevel = 0;
        }
        
        public void StartNextGame()
        {
            _currentLevel++;
            _mazeGeneration.GenerateMaze(3 + 2 * _currentLevel, 3 + 2 * _currentLevel);
            GameStartRequested.Invoke();
        }
        
        public void StopCurrentGame()
        {
            GameStopRequested.Invoke();
        }
    }
}