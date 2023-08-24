using System;

namespace Modules.GameController.Facade
{
    public interface IGameControllerFacade
    {
        event Action GameStartRequested;
        public event Action<bool> LevelDone;

        void StartNextLevel();
        void Restart();

        void StopCurrentGame(bool isWin = false);
    }
}