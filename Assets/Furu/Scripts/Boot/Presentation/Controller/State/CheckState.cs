using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Boot.Presentation.View;
using Furu.Common;
using Furu.Common.Domain.UseCase;

namespace Furu.Boot.Presentation.Controller
{
    public sealed class CheckState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly SceneUseCase _sceneUseCase;
        private readonly UpdateView _updateView;

        public CheckState(LoadingUseCase loadingUseCase, SceneUseCase sceneUseCase, UpdateView updateView)
        {
            _loadingUseCase = loadingUseCase;
            _sceneUseCase = sceneUseCase;
            _updateView = updateView;
        }

        public override BootState state => BootState.Check;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _updateView.HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            _loadingUseCase.Set(true);

            // TODO: バージョンチェック
            // TODO: WebGLではチェックしない
            await UniTask.Yield(token);

            _loadingUseCase.Set(false);

            _sceneUseCase.Load(SceneName.Main, LoadType.Direct);
            return BootState.None;
        }
    }
}