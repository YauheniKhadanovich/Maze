using System;
using TMPro;
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
        [SerializeField] 
        private Transform _headerGamePanel;
        [SerializeField] 
        private TMP_Text _timer;
        [SerializeField] 
        private TMP_Text _levelText;
        
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
            _headerGamePanel.gameObject.SetActive(false);
        }

        private void OnStartClicked()
        {
            StartButtonClicked.Invoke();
            _startButton.gameObject.SetActive(false);   
        }
        
        private void OnContinueClicked()
        {
            ContinueButtonClicked.Invoke();
        }
        
        private void OnRestartClicked()
        {
            RestartButtonClicked.Invoke();
        }
        
        public void ShowContinue()
        {
            _continueButton.gameObject.SetActive(true);
            _headerGamePanel.gameObject.SetActive(false);
        }
        
        public void ShowRestart()
        {
            _restartButton.gameObject.SetActive(true);
            _headerGamePanel.gameObject.SetActive(false);
        }

        public void ShowGameState(int levelNum)
        {
            _continueButton.gameObject.SetActive(false);
            _restartButton.gameObject.SetActive(false);   
            _headerGamePanel.gameObject.SetActive(true);
            _levelText.text = $"Level {levelNum}";
        }
        
        public void UpdateTimer(int timeInSeconds)
        {
            _timer.text = $"{timeInSeconds / 60:00}:{timeInSeconds % 60:00}";
        }
    }
}