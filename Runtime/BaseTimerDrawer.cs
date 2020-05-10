namespace Ezphera.TimerScore
{
    using UnityEngine;

    public class BaseTimerDrawer : MonoBehaviour
    {
        public Timer timer;

        // Update is called once per frame
        protected virtual void Update()
        {
            if (timer == null) return;
            DrawTimer(timer.GetTimeText(), timer.GetFill());
        }
        public virtual void DrawTimer(string timeString, float fillAmount){}
    }
}