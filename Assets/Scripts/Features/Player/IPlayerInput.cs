using System;

namespace Features.Player
{
    public interface IPlayerInput
    {
        public event Action OnRight;
        public event Action OnLeft;
        public event Action OnForward;
        public event Action OnBack;
    }
}