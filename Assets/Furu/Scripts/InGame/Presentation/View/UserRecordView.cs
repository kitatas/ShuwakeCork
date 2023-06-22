using TMPro;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class UserRecordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentScore = default;
        [SerializeField] private TextMeshProUGUI highScore = default;
        [SerializeField] private TweetButtonView distanceTweetButton = default;

        public void SetDistanceScore(float current, float high)
        {
            SetCurrentScore(current);
            SetHighScore(high);
            distanceTweetButton.SetUpDistanceTweet(current);
        }

        private void SetCurrentScore(float value)
        {
            currentScore.text = $"{value.ToString("F2")}";
        }

        private void SetHighScore(float value)
        {
            highScore.text = $"{value.ToString("F2")}";
        }
    }
}