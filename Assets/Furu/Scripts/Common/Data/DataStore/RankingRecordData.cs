using System.Collections.Generic;
using System.Linq;
using Furu.Common.Data.Entity;
using PlayFab.ClientModels;

namespace Furu.Common.Data.DataStore
{
    public sealed class RankingRecordData
    {
        private readonly List<PlayerLeaderboardEntry> _leaderboard;
        private readonly RankingType _type;

        public RankingRecordData(List<PlayerLeaderboardEntry> leaderboard, RankingType type)
        {
            _leaderboard = leaderboard;
            _type = type;
        }

        public List<DistanceRecordEntity> GetDistanceRanking(string userId)
        {
            if (_type != RankingType.Distance)
            {
                throw new CrashException(ExceptionConfig.UNMATCHED_RANKING_TYPE);
            }

            return _leaderboard
                .Select(x => new DistanceRecordEntity(x, userId))
                .ToList();
        }
    }
}