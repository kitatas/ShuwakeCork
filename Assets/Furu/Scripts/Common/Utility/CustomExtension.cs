namespace Furu.Common
{
    public static class CustomExtension
    {
        public static string ToRankingKey(this RankingType type)
        {
            return type switch
            {
                RankingType.Distance => PlayFabConfig.RANKING_DISTANCE_KEY,
                _ => throw new CrashException(ExceptionConfig.UNMATCHED_RANKING_TYPE),
            };
        }
    }
}