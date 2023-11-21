using System;

namespace Features.MazeManagement
{
    public interface ILevelTimer
    {
        event Action TimeOut;
        event Action<int> TimeTick;
        
        void StartTimer(int timeInSeconds);
        void StopTimer();
    }
}