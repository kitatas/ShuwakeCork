using System.Threading;
using Cysharp.Threading.Tasks;
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

        public async UniTask SendScoreAsync(float distance, CancellationToken token)
        {
            var playEntity = _userEntity.playEntity.UpdateByPlay(distance);
            await UniTask.WhenAll(
                _playFabRepository.UpdatePlayRecordAsync(playEntity, token),
                _playFabRepository.SendToDistanceRankingAsync(playEntity, token)
            );

            _userEntity.SetPlay(playEntity);
        }

        public (RecordEntity distance, RecordEntity _) GetUserScore()
        { 
            return (
                distance: _userEntity.playEntity.distance,
                _: RecordEntity.Default()
            );
        }
    }
}