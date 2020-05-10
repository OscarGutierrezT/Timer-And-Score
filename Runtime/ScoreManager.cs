namespace Ezphera.TimerScore
{
    using UnityEngine;

    public class ScoreManager : MonoBehaviour
    {

        public delegate void ScoreCallback(float oldScore, float newScore);

        /// <summary>
        /// This action is called when score has changed
        /// </summary>
        public event ScoreCallback OnChanged;

        /// <summary>
        /// The Score Manager in scene.
        /// </summary>
        public static ScoreManager instance;

        private void Awake()
        {
            instance = this;
        }

        private float _score
        {
            set
            {
                if (value <= 0) return;
                if (OnChanged != null) OnChanged(score, value);
                score = value;
            }
            get
            {
                return score;
            }
        }
        private float score { get; set; }

        /// <summary>
        /// Set the score
        /// </summary>
        /// <param name="newScore">New score value</param>
        public void SetScore(float newScore)
        {
            _score = newScore;
        }

        /// <summary>
        /// Add value to the current score
        /// </summary>
        /// <param name="scoreToAdd">Value to add</param>
        public void Add(float scoreToAdd = 1)
        {
            _score += scoreToAdd;
        }

        /// <summary>
        /// Return the actual score
        /// </summary>
        /// <returns></returns>
        public float GetScore()
        {
            return score;
        }
    }
}