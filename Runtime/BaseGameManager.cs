namespace Ezphera.TimerScore
{
    using UnityEngine;
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Timer))]
    [RequireComponent(typeof(ScoreManager))]
    public class BaseGameManager : MonoBehaviour
    {

        /// <summary>
        /// The BaseGameManager in scene
        /// </summary>
        public static BaseGameManager instance;

        /// <summary>
        /// The timer controller
        /// </summary>
        public Timer timer;

        /// <summary>
        /// The score manager
        /// </summary>
        public ScoreManager scoreManager;

        protected virtual void Awake()
        {
            instance = this;
            if (timer == null) timer = Timer.instance;
            if (scoreManager == null) scoreManager = ScoreManager.instance;
        }

        protected virtual void OnEnable()
        {
            timer.OnPlay += OnPlayTimer; //Callback on start timer
            timer.OnPaused += OnPausedTimer; //Callback on pause
            timer.OnRelease += OnReleasedTimer; //Callback on release
            timer.OnEnd += OnEndTimer; //Callback on End timer
            timer.OnStop += OnStopTimer; //Callback on stop the timer
            scoreManager.OnChanged += OnScoreChanged; //Callback on pause
        }
        protected virtual void OnDisable()
        {
            timer.OnPlay -= OnPlayTimer;
            timer.OnPaused -= OnPausedTimer;
            timer.OnRelease -= OnReleasedTimer;
            timer.OnEnd -= OnEndTimer;
            timer.OnStop -= OnStopTimer;
            scoreManager.OnChanged -= OnScoreChanged;
        }

        public virtual void StartGame()
        {
            timer.Play();
        }

        protected virtual void OnPlayTimer() { }

        protected virtual void OnPausedTimer() { }

        protected virtual void OnReleasedTimer() { }

        protected virtual void OnStopTimer() { }

        protected virtual void OnEndTimer() { }

        protected virtual void OnScoreChanged(float oldScore, float newScore) { }

        public virtual void AddScore(float score) { scoreManager.Add(score); }

    }
}