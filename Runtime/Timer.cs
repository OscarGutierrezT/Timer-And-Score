namespace Ezphera.TimerScore
{
    using System;
    using System.Collections;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class Timer : MonoBehaviour
    {
        #region callback vars
        public delegate void TimerCallback();

        /// <summary>
        /// CallBack on play time
        /// </summary>
        public event TimerCallback OnPlay;

        /// <summary>
        /// CallBack on paused time
        /// </summary>
        public event TimerCallback OnPaused;

        /// <summary>
        /// CallBack on release time
        /// </summary>
        public event TimerCallback OnRelease;

        /// <summary>
        /// CallBack on end time
        /// </summary>
        public event TimerCallback OnEnd;

        /// <summary>
        /// CallBack on stop time
        /// </summary>
        public event TimerCallback OnStop;
        #endregion
        public enum TimerType 
        {
            Countdown,
            Chronometer
        }
        /// <summary>
        /// Timer behaviour's
        /// </summary>
        public TimerType timerType = TimerType.Countdown;

        /// <summary>
        /// If you are using only one timer in scene this return the timer using, 
        /// but if you are using multiple timers search for other options..
        /// </summary>
        public static Timer instance;

        /// <summary>
        /// This determine if the timer is running
        /// </summary>
        public bool isPlaying;

        /// <summary>
        /// The game time in seconds
        /// </summary>
        [Tooltip("The time where start when the timer type is countdown")]
        public float startFrom = 100;

        bool paused;

        public float timeElapsed;

        private void Awake()
        {
            instance = this;
            if(timerType == TimerType.Countdown)timeElapsed = startFrom;
        }

        public void Update()
        {

            if (timerType == TimerType.Countdown)
            {
                if (isPlaying && !paused)
                {
                    if (timeElapsed > 0)
                    {
                        timeElapsed -= Time.deltaTime;
                    }
                    else
                    {
                        timeElapsed = 0;
                        isPlaying = false;
                        OnEnd?.Invoke();
                    }
                }
            }
            else if (timerType == TimerType.Chronometer) 
            {
                if (isPlaying && !paused)
                {
                    timeElapsed += Time.deltaTime;
                }
            }
        }

        public void Play()
        {
            if (!isPlaying)
            {
                if (timerType == TimerType.Countdown) timeElapsed = startFrom;
                OnPlay();
                isPlaying = true;
            }
            else if (isPlaying && paused)
            {
                paused = false;
                if (OnRelease != null) OnRelease();
            }
        }

        public void Pause()
        {
            if (isPlaying)
            {
                paused = true;
                if (OnPaused != null) OnPaused();
            }
        }
        public void Stop()
        {
            if (isPlaying)
            {
                isPlaying = false;
                paused = false;
                timeElapsed = 0;
                if (OnStop != null) OnStop();
            }
        }

        public string GetTimeText(bool clockFormat = true, float multipliedBy = 1, bool toInt = false)
        {
            var t = timeElapsed;
            if (!clockFormat)
            {
                if (multipliedBy < 1f) multipliedBy = 1f;

                var timeText = toInt ? Mathf.FloorToInt(t * multipliedBy) : t * multipliedBy;
                return timeText.ToString();
            }
            else
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(t);
                var min = startFrom >= 60 ? timeSpan.Minutes.ToString("D2") + ":" : "";
                return min + timeSpan.Seconds.ToString("D2") + ":" + (timeSpan.Milliseconds / 10).ToString("D2");
            }
        }
        public float GetFill()
        {
            return Mathf.Clamp01(timeElapsed / startFrom);
        }
    }
}