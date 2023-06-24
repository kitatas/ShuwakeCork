using Furu.Base.Presentation.View;
using UnityEngine;
using UnityEngine.Networking;

namespace Furu.InGame.Presentation.View
{
    public sealed class TweetButtonView : BaseButtonView
    {
        public void SetUpDistanceTweet(float value)
        {
            var tweetText = $"{value.ToString("F2")}m 吹っ飛ばした！\n";
#if UNITY_WEBGL
            tweetText += $"#{GameConfig.GAME_ID} #unity1week\n";
            pushed += () => UnityRoomTweet.Tweet(GameConfig.GAME_ID, tweetText);
#elif UNITY_ANDROID
            tweetText += $"#{GameConfig.GAME_ID}\n";
            // TODO: tweetText += $"{UrlConfig.APP}";
            var url = $"https://twitter.com/intent/tweet?text={UnityWebRequest.EscapeURL(tweetText)}";
            pushed += () => Application.OpenURL(url);
#endif
        }
        
        public void SetUpHeightTweet(float value)
        {
            var tweetText = $"高さ {value.ToString("F2")}m まで吹っ飛ばした！\n";
#if UNITY_WEBGL
            tweetText += $"#{GameConfig.GAME_ID} #unity1week\n";
            pushed += () => UnityRoomTweet.Tweet(GameConfig.GAME_ID, tweetText);
#elif UNITY_ANDROID
            tweetText += $"#{GameConfig.GAME_ID}\n";
            // TODO: tweetText += $"{UrlConfig.APP}";
            var url = $"https://twitter.com/intent/tweet?text={UnityWebRequest.EscapeURL(tweetText)}";
            pushed += () => Application.OpenURL(url);
#endif
        }
    }
}