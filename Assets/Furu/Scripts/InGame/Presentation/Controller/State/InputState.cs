using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.InGame.Domain.UseCase;
using UnityEngine;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class InputState : BaseState
    {
        private readonly TimeUseCase _timeUseCase;

        public InputState(TimeUseCase timeUseCase)
        {
            _timeUseCase = timeUseCase;
        }

        public override GameState state => GameState.Input;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            while (true)
            {
                if (_timeUseCase.IsTimeUp())
                {
                    break;
                }

                var deltaTime = Time.deltaTime;
                _timeUseCase.Decrease(deltaTime, GameConfig.SHAKE_TIME);

                await UniTask.Yield(token);
            }

            return GameState.Burst;
        }
    }
}