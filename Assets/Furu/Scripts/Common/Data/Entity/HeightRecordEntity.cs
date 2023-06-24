using PlayFab.ClientModels;

namespace Furu.Common.Data.Entity
{
    public sealed class HeightRecordEntity : RankingRecordEntity
    {
        public HeightRecordEntity(PlayerLeaderboardEntry entry, string userId) : base(entry, userId)
        {
        }

        protected override RankingType type => RankingType.Height;

        public override float GetScore()
        {
            return (base.GetScore() / PlayFabConfig.SCORE_RATE);
        }
    }
}