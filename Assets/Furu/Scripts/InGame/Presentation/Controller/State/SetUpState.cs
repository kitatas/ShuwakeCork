using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;
using Furu.InGame.Domain.UseCase;
using Furu.InGame.Presentation.View;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class SetUpState : BaseState
    {
        private readonly StateUseCase _stateUseCase;
        private readonly BottleView _bottleView;

        public SetUpState(StateUseCase stateUseCase, BottleView bottleView)
        {
            _stateUseCase = stateUseCase;
            _bottleView = bottleView;
        }

        public override GameState state => GameState.SetUp;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _bottleView.Init(_stateUseCase.IsState);

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _bottleView.ShowAsync(UiConfig.ANIMATION_TIME, token);

            return GameState.None;
        }
    }
}