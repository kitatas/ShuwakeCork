using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;
using Furu.InGame.Domain.UseCase;
using Furu.InGame.Presentation.View;
using UnityEngine;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class AngleState : BaseState
    {
        private readonly StateUseCase _stateUseCase;
        private readonly TimeUseCase _timeUseCase;
        private readonly ArrowView _arrowView;
        private readonly BottleView _bottleView;

        public AngleState(StateUseCase stateUseCase, TimeUseCase timeUseCase, ArrowView arrowView,
            BottleView bottleView)
        {
            _stateUseCase = stateUseCase;
            _timeUseCase = timeUseCase;
            _arrowView = arrowView;
            _bottleView = bottleView;
        }

        public override GameState state => GameState.Angle;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _arrowView.InitAsync(_stateUseCase.IsState, token).Forget();
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // 初期位置に戻す
            await _bottleView.ResetPositionAsync(UiConfig.ANIMATION_TIME, token);
            
            // angle 調整の SetUp
            await _timeUseCase.IncreaseAsync(UiConfig.ANIMATION_TIME, token);
            await _arrowView.ShowAsync(UiConfig.ANIMATION_TIME, token);

            while (true)
            {
                if (_timeUseCase.IsTimeUp())
                {
                    break;
                }

                var deltaTime = Time.deltaTime;
                _timeUseCase.Decrease(deltaTime, GameConfig.ANGLE_TIME);

                _bottleView.SyncAngle(_arrowView.angleZ);

                await UniTask.Yield(token);
            }

            await _arrowView.HideAsync(UiConfig.ANIMATION_TIME, token);

            return GameState.Burst;
        }
    }
}