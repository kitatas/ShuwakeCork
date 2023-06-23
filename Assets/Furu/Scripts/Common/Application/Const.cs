namespace Furu.Common
{
    public sealed class UiConfig
    {
        public const float ANIMATION_TIME = 0.5f;
        public const float POPUP_TIME = 0.25f;
    }

    public sealed class PlayFabConfig
    {
        public const string TITLE_ID = "";
        public const string RANKING_DISTANCE_KEY = "";
        public const string USER_PLAY_RECORD_KEY = "";
        public const int SCORE_RATE = 10000;
        public const int SHOW_MAX_RANKING = 50;
    }

    public sealed class SaveKeyConfig
    {
        public const string ES3_KEY = "";
    }

    public sealed class SoundConfig
    {
        public const float INIT_VOLUME = 5.0f;
    }

    public sealed class SceneConfig
    {
        public const float FADE_TIME = 0.5f;
    }

    public sealed class ExceptionConfig
    {
        // retry
        public const string FAILED_LOGIN = "FAILED_LOGIN";
        public const string FAILED_UPDATE_DATA = "FAILED_UPDATE_DATA";
        public const string FAILED_RESPONSE_DATA = "FAILED_RESPONSE_DATA";
        public const string UNMATCHED_USER_NAME_RULE = "UNMATCHED_USER_NAME_RULE";

        // reboot
        public const string NOT_FOUND_DATA = "NOT_FOUND_DATA";

        // crash
        public const string NOT_FOUND_STATE = "NOT_FOUND_STATE";
        public const string UNMATCHED_LOAD_TYPE = "UNMATCHED_LOAD_TYPE";
        public const string UNMATCHED_RANKING_TYPE = "UNMATCHED_RANKING_TYPE";
    }
}