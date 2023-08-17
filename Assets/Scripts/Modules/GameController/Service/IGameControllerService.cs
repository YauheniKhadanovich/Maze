using System;

namespace Modules.GameController.Service
{
    public interface IGameControllerService
    {
        event Action GameStarted;
        
        public void StartNextGame();
    }
}