using System;

namespace Modules.GameController.Service
{
    public interface IGameControllerService
    {
        event Action GameStartRequested;
        event Action<bool> LevelDone;


        public void StartNextLevel();
        void Restart();
        void StopCurrentGame(bool isWin);
    }
}