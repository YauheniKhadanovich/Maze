using Modules.GameController.Service;
using Zenject;

namespace Features.UI.Presenters.Impl
{
    public class GameViewPresenter : IGameViewPresenter
    {
        [Inject] 
        private readonly IGameControllerService _gameControllerService;
        
        public void OnStartClicked()
        {
            _gameControllerService.StartNextGame();
        }
    }
}