using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;
using Furu.Common.Domain.UseCase;
using Furu.InGame.Presentation.View;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class ResultState : BaseState
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly ReloadButtonView _reloadButtonView;

        public ResultState(SceneUseCase sceneUseCase, ReloadButtonView reloadButtonView)
        {
            _sceneUseCase = sceneUseCase;
            _reloadButtonView = reloadButtonView;
        }

        public override GameState state => GameState.Result;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _reloadButtonView.HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // TODO: ランキング表示

            await _reloadButtonView.ShowAsync(UiConfig.ANIMATION_TIME, token);
            await _reloadButtonView.PushAsync(token);

            _sceneUseCase.Load(SceneName.Main);

            return GameState.None;
        }
    }
}