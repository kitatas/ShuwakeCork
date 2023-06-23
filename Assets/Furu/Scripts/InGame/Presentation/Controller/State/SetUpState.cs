using System;
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
        private readonly CorkView _corkView;
        private readonly LiquidView _liquidView;

        public SetUpState(StateUseCase stateUseCase, BottleView bottleView, CorkView corkView, LiquidView liquidView)
        {
            _stateUseCase = stateUseCase;
            _bottleView = bottleView;
            _corkView = corkView;
            _liquidView = liquidView;
        }

        public override GameState state => GameState.SetUp;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _bottleView.Init(_stateUseCase.IsState);
            _corkView.Init(_stateUseCase.IsState);

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // ボトルに注ぐ
            await _liquidView.PourAsync(BottleConfig.POUR_TIME, token);
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            // 栓をする
            await _corkView.CloseAsync(BottleConfig.CLOSE_TIME, token);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            await _bottleView.VibrateAsync(BottleConfig.VIBRATE_TIME, token);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            // 傾ける
            await _bottleView.RotateAsync(UiConfig.ANIMATION_TIME, token);

            return GameState.Input;
        }
    }
}