using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Features.MazeManagement.Impl
{
    public class LevelTimer : MonoBehaviour, ILevelTimer, IInitializable, IDisposable
    {
        private float _currentLevelTime = 0f;

        private Coroutine _timerCoroutine;
        public event Action TimeOut = delegate { };
        public event Action<int> TimeTick = delegate { };

        public void Initialize()
        {
            //
        }

        public void Dispose()
        {
            //
        }

        public void StartTimer(int timeInSeconds)
        {
            StopTimer();
            _timerCoroutine = StartCoroutine(LevelTimerCoroutine(timeInSeconds));
        }

        public void StopTimer()
        {
            if (_timerCoroutine != null)
            {
                StopCoroutine(_timerCoroutine);
            }
        }
        
        private IEnumerator LevelTimerCoroutine(int timeInSeconds)
        {
            for (var i = timeInSeconds; i > 0; i--)
            {
                TimeTick.Invoke(i);
                yield return new WaitForSeconds(1);
            }

            TimeOut.Invoke();
        }
    }
}