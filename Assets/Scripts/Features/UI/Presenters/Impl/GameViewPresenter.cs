using System;
using Features.UI.Views;
using Modules.GameController.Facade;
using Zenject;

namespace Features.UI.Presenters.Impl
{
    public class GameViewPresenter : IGameViewPresenter, IInitializable, IDisposable
    {
        [Inject] 
        private readonly IGameControllerFacade _gameControllerFacade;
        [Inject] 
        private readonly IGameView _gameView;
        
        public void Initialize()
        {
            _gameView.StartButtonClicked += OnStartClicked;
            _gameControllerFacade.GameStopRequested += OnGameStopRequested;
        }

        public void Dispose()
        {
            _gameView.StartButtonClicked -= OnStartClicked;
            _gameControllerFacade.GameStopRequested -= OnGameStopRequested;
        }
        
        public void OnStartClicked()
        {
            _gameControllerFacade.StartNextGame();
        }
        
        private void OnGameStopRequested()
        {
            _gameView.ShowMenu();
        }
    }
}