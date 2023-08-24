using System;
using Modules.GameController.Service;
using Zenject;

namespace Modules.GameController.Facade.Impl
{
    public class GameControllerFacade : IGameControllerFacade, IInitializable
    {
        [Inject] 
        private readonly IGameControllerService _gameControllerService;
        
        public event Action GameStartRequested = delegate { };
        public event Action<bool> LevelDone = delegate { };

        public void Initialize()
        {
            _gameControllerService.GameStartRequested += OnGameStartRequested;
            _gameControllerService.LevelDone += OnLevelDone;
        }
        
        private void OnLevelDone(bool isWin)
        {
            LevelDone.Invoke(isWin);
        }

        private void OnGameStartRequested()
        {
            GameStartRequested.Invoke();
        }

        public void StartNextLevel()
        {
            _gameControllerService.StartNextLevel();
        }
        
        public void Restart()
        {
            _gameControllerService.Restart();
        }

        public void StopCurrentGame(bool isWin = false)
        {
            _gameControllerService.StopCurrentGame(isWin);
        }
    }
}