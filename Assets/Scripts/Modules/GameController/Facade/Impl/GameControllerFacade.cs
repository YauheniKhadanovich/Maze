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
        public event Action GameStopRequested = delegate { };

        public void Initialize()
        {
            _gameControllerService.GameStartRequested += OnGameStartRequested;
            _gameControllerService.GameStopRequested += OnGameStopRequested;
        }

        private void OnGameStartRequested()
        {
            GameStartRequested.Invoke();
        }
        
        private void OnGameStopRequested()
        {
            GameStopRequested.Invoke();
        }

        public void StartNextGame()
        {
            _gameControllerService.StartNextGame();
        }
        
        public void StopCurrentGame()
        {
            _gameControllerService.StopCurrentGame();
        }
    }
}