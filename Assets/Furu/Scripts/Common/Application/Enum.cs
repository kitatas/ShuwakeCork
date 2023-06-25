namespace Furu.Common
{
    public enum SceneName
    {
        None,
        Boot,
        Main,
    }

    public enum LoadType
    {
        None,
        Direct,
        Fade,
    }

    public enum RankingType
    {
        None,
        Distance,
        Height,
    }

    public enum ExceptionType
    {
        None,
        Cancel,
        Retry,
        Reboot,
        Crash,
    }

    public enum BgmType
    {
        None,
        Title,
    }

    public enum SeType
    {
        None,
        Decision,
        Pour,
        Throw,
        TempClose,
        Close,
        Timer,
        Shake,
        Burst,
        Ground,
        Finish,
        Result,
        Bonus,
    }
}