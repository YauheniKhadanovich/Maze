using System;
using Modules.Core;

namespace Modules.GameController.Facade
{
    public interface IGameControllerFacade
    {
        event Action LevelBuildRequested;
        event Action GameStarted;
        public event Action<LevelResult> LevelDone;
        public event Action<int> TimeUpdated;

        void OnContinueClicked();
        void OnRestartClicked();
        void ReportOutOfTime();
        void ReportPlayerFailed();
        void ReportDiamondTaken();
        void ReportTimerTick(int timeInSeconds);
        void ReportGameStarted(int mazeDataDiamondCount, int mazeDataTimeForMaze);
    }
}