using System;

namespace Modules.GameController.Facade
{
    public interface IGameControllerFacade
    {
        event Action GameStarted;
        
        void StartNextGame();
    }
}