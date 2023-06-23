using PlayFab.ClientModels;

namespace Furu.Common.Data.Entity
{
    public sealed class DistanceRecordEntity : RankingRecordEntity
    {
        public DistanceRecordEntity(PlayerLeaderboardEntry entry, string userId) : base(entry, userId)
        {
        }

        protected override RankingType type => RankingType.Distance;

        public override float GetScore()
        {
            return (base.GetScore() / PlayFabConfig.SCORE_RATE);
        }
    }
}