using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Views.Impl
{
    public class GameView : MonoBehaviour, IGameView
    {
        public event Action StartButtonClicked = delegate { };

        [SerializeField] 
        private Button _startButton;
        
        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClicked);
        }

        private void OnStartClicked()
        {
            StartButtonClicked.Invoke();
            _startButton.gameObject.SetActive(false);   
        }
        
        public void ShowMenu()
        {
            _startButton.gameObject.SetActive(true);
        }
    }
}