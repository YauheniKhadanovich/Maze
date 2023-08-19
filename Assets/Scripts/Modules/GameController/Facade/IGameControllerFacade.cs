using System;

namespace Modules.GameController.Facade
{
    public interface IGameControllerFacade
    {
        event Action GameStartRequested;
        event Action GameStopRequested;
        
        void StartNextGame();

        void StopCurrentGame();
    }
}