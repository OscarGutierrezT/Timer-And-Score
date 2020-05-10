namespace Ezphera.TimerScore
{
    using UnityEngine;
    using UnityEngine.UI;

    public class GameTimerAndScoreUI : MonoBehaviour
    {

        [Header("Game Timer UI Config")]
        public Text timeText;
        public Timer gameTimer = Timer.instance;
        [Header("Game Timer UI Config")]
        public Text scoreText;
        public ScoreManager scoreManager = ScoreManager.instance;
        private void OnEnable()
        {
            scoreManager.OnChanged += ScoreManager_OnChanged;
            scoreText.text = scoreManager.GetScore().ToString();
        }

        private void ScoreManager_OnChanged(float oldScore, float newScore)
        {
            scoreText.text = newScore.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            if (timeText != null) timeText.text = gameTimer.GetTimeText();
        }
    }
}