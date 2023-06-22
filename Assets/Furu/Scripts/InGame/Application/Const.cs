namespace Furu.InGame
{
    public sealed class GameConfig
    {
        // TODO: 新規IDに修正する
        public const string GAME_ID = "granasd_alime";
        
        public const GameState INIT_STATE = GameState.Title;

        public const float SHAKE_TIME = 10.0f;
    }

    public sealed class TagConfig
    {
        public const string GROUND = "Ground";
    }
}