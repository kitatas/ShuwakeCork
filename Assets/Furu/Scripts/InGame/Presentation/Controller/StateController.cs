using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class StateController
    {
        private readonly List<BaseState> _states;

        public StateController(TitleState titleState, SetUpState setUpState, InputState inputState,
            AngleState angleState, BurstState burstState, FinishState finishState, ResultState resultState)
        {
            _states = new List<BaseState>
            {
                titleState,
                setUpState,
                inputState,
                angleState,
                burstState,
                finishState,
                resultState,
            };
        }

        public async UniTaskVoid InitAsync(CancellationToken token)
        {
            foreach (var state in _states)
            {
                state.InitAsync(token).Forget();
            }

            await UniTask.Yield(token);
        }

        public async UniTask<GameState> TickAsync(GameState state, CancellationToken token)
        {
            var currentState = _states.Find(x => x.state == state);
            if (currentState == null)
            {
                throw new CrashException(ExceptionConfig.NOT_FOUND_STATE);
            }

            return await currentState.TickAsync(token);
        }
    }
}