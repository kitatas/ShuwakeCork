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
        private readonly UserDataUseCase _userDataUseCase;
        private readonly BonusView _bonusView;
        private readonly RankingView _rankingView;

        public ResultState(LoadingUseCase loadingUseCase, RankingUseCase rankingUseCase, SceneUseCase sceneUseCase,
            UserDataUseCase userDataUseCase, BonusView bonusView, RankingView rankingView)
        {
            _loadingUseCase = loadingUseCase;
            _rankingUseCase = rankingUseCase;
            _sceneUseCase = sceneUseCase;
            _userDataUseCase = userDataUseCase;
            _bonusView = bonusView;
            _rankingView = rankingView;
        }

        public override GameState state => GameState.Result;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _bonusView.HideAsync(0.0f, token).Forget();
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

            // プレイボーナス
            if (_userDataUseCase.GetPlayCount() == GameConfig.PLAY_BONUS)
            {
                await _bonusView.PopAsync(UiConfig.POPUP_TIME, token);
            }

            _sceneUseCase.Load(SceneName.Main, LoadType.Fade);

            return GameState.None;
        }
    }
}