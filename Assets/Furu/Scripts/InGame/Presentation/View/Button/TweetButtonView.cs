using Furu.Base.Presentation.View;
using Furu.Common;
using UnityEngine;
using UnityEngine.Networking;

namespace Furu.InGame.Presentation.View
{
    public sealed class TweetButtonView : BaseButtonView
    {
        public void SetUpDistanceTweet(float value, string liquidName)
        {
            var tweetText = $"{liquidName}で距離 {value.ToString("F2")}m 吹っ飛ばした！\n";
            Tweet(tweetText);
        }

        public void SetUpHeightTweet(float value, string liquidName)
        {
            var tweetText = $"{liquidName}で高さ {value.ToString("F2")}m まで吹っ飛ばした！\n";
            Tweet(tweetText);
        }

        private void Tweet(string tweetText)
        {
#if UNITY_WEBGL
            // tweetText += $"#{GameConfig.GAME_ID} #unity1week\n";
            // pushed += () => UnityRoomTweet.Tweet(GameConfig.GAME_ID, tweetText);
#elif UNITY_ANDROID
            tweetText += $"#{GameConfig.GAME_ID}\n";
            tweetText += $"{UrlConfig.APP}";
            var url = $"https://twitter.com/intent/tweet?text={UnityWebRequest.EscapeURL(tweetText)}";
            pushed += () => Application.OpenURL(url);
#endif
        }
    }
}