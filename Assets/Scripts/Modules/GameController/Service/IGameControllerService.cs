using System;
using Modules.Core;

namespace Modules.GameController.Service
{
    public interface IGameControllerService
    {
        event Action LevelBuildRequested;
        event Action<LevelResult> LevelDone;
        event Action GameStarted;

        public void StartNextLevel();
        void Restart();
        void ReportDiamondTaken();
        void ReportGameStarted(int mazeDataDiamondCount, int mazeDataTimeForMaze);
        void TimerTick();
        void ReportOutOfTime();
        void ReportPlayerFailed();
    }
}