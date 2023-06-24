using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;
using Furu.Common.Data.Entity;
using Furu.Common.Domain.Repository;

namespace Furu.InGame.Domain.UseCase
{
    public sealed class UserRecordUseCase
    {
        private readonly UserEntity _userEntity;
        private readonly PlayFabRepository _playFabRepository;

        public UserRecordUseCase(UserEntity userEntity, PlayFabRepository playFabRepository)
        {
            _userEntity = userEntity;
            _playFabRepository = playFabRepository;
        }

        public async UniTask SendScoreAsync(float distance, float height, CancellationToken token)
        {
            var playEntity = _userEntity.playEntity.UpdateByPlay(distance, height);
            await UniTask.WhenAll(
                _playFabRepository.UpdatePlayRecordAsync(playEntity, token),
                _playFabRepository.SendRankingAsync(RankingType.Distance, playEntity, token),
                _playFabRepository.SendRankingAsync(RankingType.Height, playEntity, token)
            );

            _userEntity.SetPlay(playEntity);
        }

        public (RecordEntity distance, RecordEntity height) GetUserScore()
        {
            return (
                distance: _userEntity.playEntity.distance,
                height: _userEntity.playEntity.height
            );
        }
    }
}