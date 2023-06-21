using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.InGame.Presentation.View;
using UnityEngine;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class BurstState : BaseState
    {
        private readonly BottleView _bottleView;
        private readonly CorkView _corkView;

        public BurstState(BottleView bottleView, CorkView corkView)
        {
            _bottleView = bottleView;
            _corkView = corkView;
        }

        public override GameState state => GameState.Burst;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // TODO: 発射演出
            // TODO: 角度調整
            var angle = new Vector2(1.0f, 1.0f);
            _corkView.Shot(_bottleView.shakePower * 10.0f * angle);
            _bottleView.ShowSplash();

            await UniTask.WaitUntil(_corkView.IsStop, cancellationToken: token);

            return GameState.Finish;
        }
    }
}