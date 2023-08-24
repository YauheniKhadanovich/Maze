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
            _gameView.RestartButtonClicked += OnRestartClicked;
            _gameView.ContinueButtonClicked += OnContinueClicked;
            _gameControllerFacade.LevelDone += LevelDone;
        }

        public void Dispose()
        {
            _gameView.StartButtonClicked -= OnStartClicked;
            _gameView.RestartButtonClicked -= OnRestartClicked;
            _gameControllerFacade.LevelDone -= LevelDone;
        }
        
        public void OnStartClicked()
        {
            _gameControllerFacade.Restart();
        }
        
        private void OnRestartClicked()
        {
            _gameControllerFacade.Restart();
        }
        
        private void OnContinueClicked()
        {
            _gameControllerFacade.StartNextLevel();
        }
        
        private void LevelDone(bool isWin)
        {
            if (isWin)
            {
                _gameView.ShowContinue();
            }
            else
            {
                _gameView.ShowRestart();
            }
        }
    }
}