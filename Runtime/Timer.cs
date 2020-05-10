namespace Ezphera.TimerScore
{
    using System;
    using System.Collections;
    using UnityEngine;

    [DisallowMultipleComponent]
    public class Timer : MonoBehaviour
    {
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

        /// <summary>
        /// The Timer on scene
        /// </summary>
        public static Timer instance;

        /// <summary>
        /// The time is running on scene
        /// </summary>
        [HideInInspector]
        public bool isPlaying;

        /// <summary>
        /// The game time in seconds
        /// </summary>
        [Tooltip("The game time in seconds")]
        public float time = 100;

        public bool timeAsScore;
        public int scoreBased;

        bool paused;
        public float timeElapsed;

        private void Awake()
        {
            instance = this;
        }

        public void SetTimeAsScore(bool asScore)
        {
            timeAsScore = asScore;
        }

        public void Play()
        {
            if (!isPlaying)
            {
                StartCoroutine("RunTimer");
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
                StopCoroutine("RunTimer");
                isPlaying = false;
                paused = false;
                timeElapsed = 0;
                if (OnStop != null) OnStop();
            }
        }

        IEnumerator RunTimer()
        {
            //Start time CallBack
            if (OnPlay != null) OnPlay();
            isPlaying = true;
            timeElapsed = 0.0f;
            while (timeElapsed <= time)
            {
                if (!paused)
                {
                    timeElapsed += Time.deltaTime;
                }
                yield return null;
            }
            // End time
            isPlaying = false;
            //CallBack
            if (OnEnd != null) OnEnd();
            yield return null;
        }

        public string GetTimeText()
        {
            var t = Mathf.Clamp(time - timeElapsed, 0.0f, time);
            if (timeAsScore)
            {
                return Mathf.FloorToInt(t * scoreBased).ToString();
            }
            else
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(t);
                var min = time >= 60 ? timeSpan.Minutes.ToString("D2") + ":" : "";
                return min + timeSpan.Seconds.ToString("D2") + ":" + (timeSpan.Milliseconds / 10).ToString("D2");
            }
        }
        public float GetFill()
        {
            return Mathf.Clamp01(timeElapsed / time);
        }
    }
}