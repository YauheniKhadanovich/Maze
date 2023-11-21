using System;
using Modules.Core;
using Modules.GameController.Service;
using UnityEngine;
using Zenject;

namespace Modules.GameController.Facade.Impl
{
    public class GameControllerFacade : IGameControllerFacade, IInitializable
    {
        [Inject] 
        private readonly IGameControllerService _gameControllerService;
        
        public event Action LevelBuildRequested = delegate { };
        public event Action GameStarted  = delegate { };
        public event Action<LevelResult> LevelDone = delegate { };
        public event Action<int> TimeUpdated = delegate { };

        public void Initialize()
        {
            _gameControllerService.LevelBuildRequested += OnLevelBuildRequested;
            _gameControllerService.LevelDone += OnLevelDone;
            _gameControllerService.GameStarted += OnGameStarted;
        }

        private void OnGameStarted()
        {
            GameStarted.Invoke();
        }

        private void OnLevelDone(LevelResult result)
        {
            LevelDone.Invoke(result);
        }

        private void OnLevelBuildRequested()
        {
            LevelBuildRequested.Invoke();
        }

        public void OnContinueClicked()
        {
            _gameControllerService.StartNextLevel();
        }
        
        public void OnRestartClicked()
        {
            _gameControllerService.Restart();
        }

        public void ReportOutOfTime()
        {
            _gameControllerService.ReportOutOfTime();
        }

        public void ReportPlayerFailed()
        {
            _gameControllerService.ReportPlayerFailed();
        }

        public void ReportDiamondTaken()
        {
            _gameControllerService.ReportDiamondTaken();
        }

        public void ReportTimerTick(int timeInSeconds)
        {
            TimeUpdated.Invoke(timeInSeconds);
        }

        public void ReportGameStarted(int mazeDataDiamondCount, int mazeDataTimeForMaze)
        {
            _gameControllerService.ReportGameStarted(mazeDataDiamondCount, mazeDataTimeForMaze);
        }
    }
}