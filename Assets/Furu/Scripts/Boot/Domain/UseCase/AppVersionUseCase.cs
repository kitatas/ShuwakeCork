using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Boot.Data.Entity;
using Furu.Common;
using Furu.Common.Domain.Repository;

namespace Furu.Boot.Domain.UseCase
{
    public sealed class AppVersionUseCase
    {
        private readonly PlayFabRepository _playFabRepository;

        public AppVersionUseCase(PlayFabRepository playFabRepository)
        {
            _playFabRepository = playFabRepository;
        }

        public async UniTask<bool> CheckUpdateAsync(CancellationToken token)
        {
            var masterData = await _playFabRepository.FetchMasterDataAsync(token);
            var appVersion = masterData.DeserializeMaster<AppVersionEntity>(PlayFabConfig.MASTER_APP_VERSION_KEY);
            return appVersion.IsForceUpdate();
        }
    }
}