using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;
using Furu.Common.Domain.UseCase;
using Furu.InGame.Domain.UseCase;
using Furu.InGame.Presentation.View;
using UniRx;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class BurstState : BaseState
    {
        private readonly FastForwardUseCase _fastForwardUseCase;
        private readonly SoundUseCase _soundUseCase;
        private readonly UserDataUseCase _userDataUseCase;
        private readonly FastForwardButtonView _fastForwardButtonView;
        private readonly ArrowView _arrowView;
        private readonly BottleView _bottleView;
        private readonly CorkView _corkView;
        private readonly LiquidView _liquidView;

        public BurstState(FastForwardUseCase fastForwardUseCase, SoundUseCase soundUseCase,
            UserDataUseCase userDataUseCase, FastForwardButtonView fastForwardButtonView, ArrowView arrowView,
            BottleView bottleView, CorkView corkView, LiquidView liquidView)
        {
            _fastForwardUseCase = fastForwardUseCase;
            _soundUseCase = soundUseCase;
            _userDataUseCase = userDataUseCase;
            _fastForwardButtonView = fastForwardButtonView;
            _arrowView = arrowView;
            _bottleView = bottleView;
            _corkView = corkView;
            _liquidView = liquidView;
        }

        public override GameState state => GameState.Burst;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _fastForwardButtonView.Hide(0.0f);
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _soundUseCase.PlaySe(SeType.Burst);
            var bonusRate = _userDataUseCase.GetPlayCount() >= GameConfig.PLAY_BONUS ? 1.5f : 1.0f;
            var power = (_bottleView.shakePower + 10.0f) * 10.0f;
            _corkView.Shot(power * bonusRate * _arrowView.direction);
            _liquidView.Splash();

            // 早送り設定
            _fastForwardButtonView.pushed += () => _fastForwardUseCase.Switch();
            _fastForwardUseCase.active
                .Subscribe(x =>
                {
                    _fastForwardUseCase.Set(x);
                    _fastForwardButtonView.SetIcon(x);
                })
                .AddTo(_fastForwardButtonView);
            _fastForwardButtonView.Show(UiConfig.ANIMATION_TIME);

            await UniTask.WaitUntil(_corkView.IsStop, cancellationToken: token);

            // default speed に戻す
            _fastForwardButtonView.Hide(UiConfig.ANIMATION_TIME);
            _fastForwardUseCase.Reset();

            _soundUseCase.PlaySe(SeType.Finish);
            return GameState.Finish;
        }
    }
}