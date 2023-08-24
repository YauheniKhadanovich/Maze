using System;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Views.Impl
{
    public class GameView : MonoBehaviour, IGameView
    {
        public event Action StartButtonClicked = delegate { };
        public event Action ContinueButtonClicked = delegate { };
        public event Action RestartButtonClicked = delegate { };

        [SerializeField] 
        private Button _startButton;
        [SerializeField] 
        private Button _restartButton;
        [SerializeField] 
        private Button _continueButton;
        
        private void Awake()
        {
            _startButton.onClick.AddListener(OnStartClicked);
            _restartButton.onClick.AddListener(OnRestartClicked);
            _continueButton.onClick.AddListener(OnContinueClicked);
        }

        private void Start()
        {
            _startButton.gameObject.SetActive(true);   
            _restartButton.gameObject.SetActive(false);   
            _continueButton.gameObject.SetActive(false);
        }

        private void OnStartClicked()
        {
            StartButtonClicked.Invoke();
            _startButton.gameObject.SetActive(false);   
        }
        
        private void OnContinueClicked()
        {
            ContinueButtonClicked.Invoke();
            _continueButton.gameObject.SetActive(false);   
        }
        
        private void OnRestartClicked()
        {
            RestartButtonClicked.Invoke();
            _restartButton.gameObject.SetActive(false);   
        }
        
        public void ShowContinue()
        {
            _continueButton.gameObject.SetActive(true);
        }
        
        public void ShowRestart()
        {
            _restartButton.gameObject.SetActive(true);
        }
    }
}