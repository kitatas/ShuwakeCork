using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;
using Furu.Common.Domain.UseCase;

namespace Furu.Boot.Presentation.Controller
{
    public sealed class CheckState : BaseState
    {
        private readonly SceneUseCase _sceneUseCase;

        public CheckState(SceneUseCase sceneUseCase)
        {
            _sceneUseCase = sceneUseCase;
        }

        public override BootState state => BootState.Check;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            // TODO: バージョンチェック
            // TODO: WebGLではチェックしない
            await UniTask.Yield(token);

            _sceneUseCase.Load(SceneName.Main);
            return BootState.None;
        }
    }
}