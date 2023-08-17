using Features.UI.Presenters;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.UI.Views.Impl
{
    public class GameView : MonoBehaviour, IGameView
    {
        [Inject] 
        private readonly IGameViewPresenter _gameViewPresenter;

        [SerializeField] 
        private Button _startButton;
        
        private void Awake()
        {
            _startButton .onClick.AddListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            _gameViewPresenter.OnStartClicked();
            _startButton.gameObject.SetActive(false);   
        }
    }
}