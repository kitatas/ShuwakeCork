using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;
using Furu.Common.Domain.UseCase;
using Furu.InGame.Domain.UseCase;
using Furu.InGame.Presentation.View;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class ResultState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly RankingUseCase _rankingUseCase;
        private readonly SceneUseCase _sceneUseCase;
        private readonly RankingView _rankingView;

        public ResultState(LoadingUseCase loadingUseCase, RankingUseCase rankingUseCase, SceneUseCase sceneUseCase,
            RankingView rankingView)
        {
            _loadingUseCase = loadingUseCase;
            _rankingUseCase = rankingUseCase;
            _sceneUseCase = sceneUseCase;
            _rankingView = rankingView;
        }

        public override GameState state => GameState.Result;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _rankingView.HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _loadingUseCase.Set(true);

            var (distanceRecords, heightRecords) = await (
                _rankingUseCase.GetDistanceRankingAsync(token),
                _rankingUseCase.GetHeightRankingAsync(token)
            );
            _rankingView.SetUp(distanceRecords, heightRecords);

            _loadingUseCase.Set(false);

            await _rankingView.ReloadAsync(UiConfig.POPUP_TIME, token);

            _sceneUseCase.Load(SceneName.Main, LoadType.Fade);

            return GameState.None;
        }
    }
}