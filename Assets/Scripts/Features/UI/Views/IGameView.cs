using System;

namespace Features.UI.Views
{
    public interface IGameView
    {
        event Action StartButtonClicked;
        void ShowMenu();
    }
}