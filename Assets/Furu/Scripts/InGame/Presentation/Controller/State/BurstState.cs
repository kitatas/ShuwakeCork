using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;
using Furu.Common.Domain.UseCase;
using Furu.InGame.Domain.UseCase;
using Furu.InGame.Presentation.View;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class BurstState : BaseState
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly UserDataUseCase _userDataUseCase;
        private readonly ArrowView _arrowView;
        private readonly BottleView _bottleView;
        private readonly CorkView _corkView;
        private readonly LiquidView _liquidView;

        public BurstState(SoundUseCase soundUseCase, UserDataUseCase userDataUseCase, ArrowView arrowView,
            BottleView bottleView, CorkView corkView, LiquidView liquidView)
        {
            _soundUseCase = soundUseCase;
            _userDataUseCase = userDataUseCase;
            _arrowView = arrowView;
            _bottleView = bottleView;
            _corkView = corkView;
            _liquidView = liquidView;
        }

        public override GameState state => GameState.Burst;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _soundUseCase.PlaySe(SeType.Burst);
            var bonusRate = _userDataUseCase.GetPlayCount() >= GameConfig.PLAY_BONUS ? 1.5f : 1.0f;
            _corkView.Shot(_bottleView.shakePower * bonusRate * 10.0f * _arrowView.direction);
            _liquidView.Splash();

            await UniTask.WaitUntil(_corkView.IsStop, cancellationToken: token);

            _soundUseCase.PlaySe(SeType.Finish);
            return GameState.Finish;
        }
    }
}