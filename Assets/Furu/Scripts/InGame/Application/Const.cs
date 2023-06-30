namespace Furu.InGame
{
    public sealed class GameConfig
    {
        public const string GAME_ID = "shuwake_cork";

        public const GameState INIT_STATE = GameState.Title;

        public const float SHAKE_TIME = 5.0f;
        public const float ANGLE_TIME = 5.0f;

        public const int PLAY_BONUS = 3;

        public const float FAST_SPEED = 10.0f;
        public const float DEFAULT_SPEED = 1.0f;
    }

    public sealed class TagConfig
    {
        public const string GROUND = "Ground";
    }

    public sealed class BottleConfig
    {
        public const float POUR_TIME = 1.5f;
        public const float CLOSE_TIME = 0.5f;
        public const float VIBRATE_TIME = 0.1f;

        public const float TEMP_CLOSE_HEIGHT = 1.2f;
        public const float CLOSE_HEIGHT = 1.068f;
    }

    public sealed class ResourceConfig
    {
        public const string BASE_PATH = "Assets/Furu/";
        public const string JSON_PATH = BASE_PATH + "Master/Json/";
    }
}