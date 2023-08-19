using System;

namespace Modules.GameController.Service
{
    public interface IGameControllerService
    {
        event Action GameStartRequested;
        event Action GameStopRequested;

        public void StartNextGame();

        void StopCurrentGame();
    }
}