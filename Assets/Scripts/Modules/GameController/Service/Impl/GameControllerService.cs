using System;
using Modules.Core;
using Modules.MazeGenerator.Facade;
using Zenject;

namespace Modules.GameController.Service.Impl
{
    public class GameControllerService : IGameControllerService, IInitializable
    {
        [Inject] private readonly IMazeGenerationFacade _mazeGeneration;

        public event Action LevelBuildRequested = delegate { };
        public event Action<int> GameStarted = delegate { };
        public event Action<LevelResult> LevelDone = delegate { };

        private int _currentLevel;
        private int _levelTime;
        private int _levelDiamonds;

        public void Initialize()
        {
            _currentLevel = 0;
        }

        public void StartNextLevel()
        {
            _currentLevel++;
            _mazeGeneration.GenerateMaze(3 + 2 * _currentLevel, 3 + 2 * _currentLevel);
            LevelBuildRequested.Invoke();
        }

        public void Restart()
        {
            _currentLevel = 0;
            StartNextLevel();
        }

        public void ReportDiamondTaken()
        {
            _levelDiamonds--;
            if (_levelDiamonds <= 0)
            {
                LevelDone.Invoke(LevelResult.Win);
            }
        }

        public void ReportGameStarted(int mazeDataDiamondCount, int mazeDataTimeForMaze)
        {
            _levelDiamonds = mazeDataDiamondCount;
            _levelTime = mazeDataTimeForMaze;
            GameStarted.Invoke(_currentLevel);
        }

        public void TimerTick()
        {
            //
        }

        public void ReportOutOfTime()
        {
            LevelDone.Invoke(LevelResult.OutOfTime);
        }

        public void ReportPlayerFailed()
        {
            LevelDone.Invoke(LevelResult.Fail);
        }
    }
}