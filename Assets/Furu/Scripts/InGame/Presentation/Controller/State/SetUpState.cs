using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;
using Furu.Common.Domain.UseCase;
using Furu.InGame.Domain.UseCase;
using Furu.InGame.Presentation.View;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class SetUpState : BaseState
    {
        private readonly LiquidUseCase _liquidUseCase;
        private readonly SoundUseCase _soundUseCase;
        private readonly StateUseCase _stateUseCase;
        private readonly TimeUseCase _timeUseCase;
        private readonly UserDataUseCase _userDataUseCase;
        private readonly BottleView _bottleView;
        private readonly CandyView _candyView;
        private readonly CorkView _corkView;
        private readonly LiquidView _liquidView;

        public SetUpState(LiquidUseCase liquidUseCase, SoundUseCase soundUseCase, StateUseCase stateUseCase,
            TimeUseCase timeUseCase, UserDataUseCase userDataUseCase, BottleView bottleView, CandyView candyView,
            CorkView corkView, LiquidView liquidView)
        {
            _liquidUseCase = liquidUseCase;
            _soundUseCase = soundUseCase;
            _stateUseCase = stateUseCase;
            _timeUseCase = timeUseCase;
            _userDataUseCase = userDataUseCase;
            _bottleView = bottleView;
            _candyView = candyView;
            _corkView = corkView;
            _liquidView = liquidView;
        }

        public override GameState state => GameState.SetUp;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _bottleView.Init(_stateUseCase.IsState, _soundUseCase.PlaySe);
            _corkView.Init(_stateUseCase.IsState, _soundUseCase.PlaySe);
            _liquidView.Init();

            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            var liquid = _liquidUseCase.Lot();
            _liquidView.SetUp(liquid);

            // ボトルに注ぐ
            _soundUseCase.PlaySe(SeType.Pour, 0.35f);
            await _liquidView.PourAsync(BottleConfig.POUR_TIME, token);
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            // ボーナス
            if (_userDataUseCase.GetPlayCount() >= GameConfig.PLAY_BONUS)
            {
                _soundUseCase.PlaySe(SeType.Throw, 1.35f);
                await _candyView.ThrowAsync(BottleConfig.POUR_TIME, token);
                await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);
            }
            else
            {
                _candyView.Activate(false);
            }

            // 栓をする
            await _corkView.CloseAsync(BottleConfig.TEMP_CLOSE_HEIGHT, BottleConfig.CLOSE_TIME, token);
            _soundUseCase.PlaySe(SeType.TempClose);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            _soundUseCase.PlaySe(SeType.Close);
            await (
                _bottleView.VibrateAsync(BottleConfig.VIBRATE_TIME, token),
                _corkView.CloseAsync(BottleConfig.CLOSE_HEIGHT, BottleConfig.VIBRATE_TIME, token)
            );
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            // 傾ける
            _soundUseCase.PlaySe(SeType.Timer);
            await (
                _timeUseCase.IncreaseAsync(UiConfig.ANIMATION_TIME, token),
                _bottleView.RotateAsync(UiConfig.ANIMATION_TIME, token)
            );

            return GameState.Input;
        }
    }
}