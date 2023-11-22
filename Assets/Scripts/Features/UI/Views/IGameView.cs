using System;

namespace Features.UI.Views
{
    public interface IGameView
    {
        event Action StartButtonClicked;
        event Action RestartButtonClicked;
        event Action ContinueButtonClicked;
        
        void ShowContinue();
        void ShowRestart();
        void UpdateTimer(int timeInSeconds);
        void ShowGameState(int levelNum);
    }
}