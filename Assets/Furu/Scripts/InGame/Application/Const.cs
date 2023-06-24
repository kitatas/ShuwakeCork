namespace Furu.InGame
{
    public sealed class GameConfig
    {
        // TODO: 新規IDに修正する
        public const string GAME_ID = "granasd_alime";
        
        public const GameState INIT_STATE = GameState.Title;

        public const float SHAKE_TIME = 5.0f;
        public const float ANGLE_TIME = 5.0f;
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