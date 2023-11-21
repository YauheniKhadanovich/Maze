using System;
using Features.UI.Views;
using Modules.Core;
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
            _gameControllerFacade.LevelDone += OnLevelDone;
            _gameControllerFacade.TimeUpdated += OnTimeUpdated;
            _gameControllerFacade.GameStarted += OnGameStarted;
        }
        
        public void Dispose()
        {
            _gameView.StartButtonClicked -= OnStartClicked;
            _gameView.RestartButtonClicked -= OnRestartClicked;
            _gameView.ContinueButtonClicked -= OnContinueClicked;
            _gameControllerFacade.LevelDone -= OnLevelDone;
            _gameControllerFacade.TimeUpdated -= OnTimeUpdated;
            _gameControllerFacade.GameStarted -= OnGameStarted;
        }

        public void OnStartClicked()
        {
            _gameControllerFacade.OnRestartClicked();
        }
        
        private void OnRestartClicked()
        {
            _gameControllerFacade.OnRestartClicked();
        }
        
        private void OnContinueClicked()
        {
            _gameControllerFacade.OnContinueClicked();
        }
        
        private void OnTimeUpdated(int time)
        {
            _gameView.UpdateTimer(time);
        }
        
        private void OnLevelDone(LevelResult result)
        {
            switch (result)
            {
                case LevelResult.Win:
                    _gameView.ShowContinue();
                    break;
                case LevelResult.Fail:
                    _gameView.ShowRestart();
                    break;
                case LevelResult.OutOfTime:
                    _gameView.ShowRestart();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(result), result, null);
            }
        }
        
        private void OnGameStarted()
        {
            _gameView.ShowGameState();
        }
    }
}