using TMPro;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class UserRecordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentScore = default;
        [SerializeField] private TextMeshProUGUI highScore = default;
        [SerializeField] private TweetButtonView distanceTweetButton = default;

        [SerializeField] private TextMeshProUGUI currentHeight = default;
        [SerializeField] private TextMeshProUGUI highHeight = default;
        [SerializeField] private TweetButtonView heightTweetButton = default;

        public void SetDistanceScore(float current, float high, string liquidName)
        {
            SetCurrentScore(current);
            SetHighScore(high);
            distanceTweetButton.SetUpDistanceTweet(current, liquidName);
        }

        private void SetCurrentScore(float value)
        {
            currentScore.text = $"{value.ToString("F2")}";
        }

        private void SetHighScore(float value)
        {
            highScore.text = $"{value.ToString("F2")}";
        }

        public void SetHeightScore(float current, float high, string liquidName)
        {
            SetCurrentHeight(current);
            SetHighHeight(high);
            heightTweetButton.SetUpHeightTweet(current, liquidName);
        }

        private void SetCurrentHeight(float value)
        {
            currentHeight.text = $"{value.ToString("F2")}";
        }

        private void SetHighHeight(float value)
        {
            highHeight.text = $"{value.ToString("F2")}";
        }
    }
}