using Modules.GameController.Facade;
using Zenject;

namespace Features.UI.Presenters.Impl
{
    public class GameViewPresenter : IGameViewPresenter
    {
        [Inject] 
        private readonly IGameControllerFacade _gameControllerFacade;
        
        public void OnStartClicked()
        {
            _gameControllerFacade.StartNextGame();
        }
    }
}