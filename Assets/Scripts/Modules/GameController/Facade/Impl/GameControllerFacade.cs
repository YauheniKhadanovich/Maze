using System;
using Modules.GameController.Service;
using Zenject;

namespace Modules.GameController.Facade.Impl
{
    public class GameControllerFacade : IGameControllerFacade, IInitializable
    {
        public event Action GameStarted = delegate { };

        [Inject] 
        private readonly IGameControllerService _gameControllerService;

        public void Initialize()
        {
            _gameControllerService.GameStarted += OnGameStarted;
        }

        private void OnGameStarted()
        {
            GameStarted.Invoke();
        }
    }
}