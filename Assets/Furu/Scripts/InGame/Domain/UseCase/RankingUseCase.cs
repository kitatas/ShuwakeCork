using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;
using Furu.Common.Data.Entity;
using Furu.Common.Domain.Repository;

namespace Furu.InGame.Domain.UseCase
{
    public sealed class RankingUseCase
    {
        private readonly UserEntity _userEntity;
        private readonly PlayFabRepository _playFabRepository;

        public RankingUseCase(UserEntity userEntity, PlayFabRepository playFabRepository)
        {
            _userEntity = userEntity;
            _playFabRepository = playFabRepository;
        }

        public async UniTask<List<DistanceRecordEntity>> GetDistanceRankingAsync(CancellationToken token)
        {
            var recordData = await _playFabRepository.GetRankDataAsync(RankingType.Distance, token);
            return recordData.GetDistanceRanking(_userEntity.userId);
        }
        
        public async UniTask<List<HeightRecordEntity>> GetHeightRankingAsync(CancellationToken token)
        {
            var recordData = await _playFabRepository.GetRankDataAsync(RankingType.Height, token);
            return recordData.GetHeightRanking(_userEntity.userId);
        }
    }
}