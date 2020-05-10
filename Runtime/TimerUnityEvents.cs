namespace Ezphera.TimerScore
{
    using UnityEngine;
    using UnityEngine.Events;
    [RequireComponent(typeof(Timer))]
    public class TimerUnityEvents : MonoBehaviour
    {
        Timer timer;
        public UnityEvent OnPlayStart, OnPaused, OnReleased, OnStop, OnEndTimer;
        private void OnEnable()
        {
            timer = GetComponent<Timer>();
            timer.OnPlay += Timer_OnPlay;
            timer.OnPaused += Timer_OnPaused;
            timer.OnRelease += Timer_OnRelease;
            timer.OnStop += Timer_OnStop;
            timer.OnEnd += Timer_OnEnd;
        }
        private void OnDisable()
        {
            timer.OnPlay -= Timer_OnPlay;
            timer.OnPaused -= Timer_OnPaused;
            timer.OnRelease -= Timer_OnRelease;
            timer.OnStop -= Timer_OnStop;
            timer.OnEnd -= Timer_OnEnd;
        }

        private void Timer_OnPlay()
        {
            OnPlayStart.Invoke();
        }

        private void Timer_OnPaused()
        {
            OnPaused.Invoke();
        }

        private void Timer_OnRelease()
        {
            OnReleased.Invoke();
        }

        private void Timer_OnStop()
        {
            OnStop.Invoke();
        }

        private void Timer_OnEnd()
        {
            OnEndTimer.Invoke();
        }
    }
}