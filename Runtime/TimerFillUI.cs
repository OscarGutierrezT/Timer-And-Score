namespace Ezphera.Examples
{
    using UnityEngine;
    using UnityEngine.UI;
    using Ezphera.TimerScore;

    public class TimerFillUI : BaseTimerDrawer
    {
        public Text timeText;
        public Image fillImage;

        public override void DrawTimer(string timeString, float fillAmount)
        {
            base.DrawTimer(timeString,fillAmount);
            timeText.text = timeString;
            fillImage.fillAmount = 1 - fillAmount;
        }
    }
}