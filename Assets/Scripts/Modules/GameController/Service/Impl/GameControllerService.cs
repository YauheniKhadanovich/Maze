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
        public event Action<bool> LevelDone = delegate { };

        private int _currentLevel;
        
        public void Initialize()
        {
            _currentLevel = 0;
        }
        
        public void StartNextLevel()
        {
            _currentLevel++;
            _mazeGeneration.GenerateMaze(3 + 2 * _currentLevel, 3 + 2 * _currentLevel);
            GameStartRequested.Invoke();
        }
        
        public void Restart()
        {
            _currentLevel = 0;
            StartNextLevel();
        }
        
        public void StopCurrentGame(bool isWin)
        {
            LevelDone.Invoke(isWin);
        }
    }
}