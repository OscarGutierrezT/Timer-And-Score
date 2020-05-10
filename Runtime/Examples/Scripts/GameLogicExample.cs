namespace Ezphera.Examples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Ezphera.TimerScore;
    public class GameLogicExample : BaseGameManager
    {

        protected override void OnPlayTimer()
        {
            base.OnPlayTimer();
            Debug.Log("Start timer");
        }

        protected override void OnEndTimer()
        {
            base.OnEndTimer();
            Debug.Log("End the current timer");
        }

        protected override void OnScoreChanged(float oldScore, float newScore)
        {
            base.OnScoreChanged(oldScore, newScore);
            Debug.Log(string.Format("The score has changed: Previous score = {0} and New score = {1}", oldScore, newScore));
        }
    }
}