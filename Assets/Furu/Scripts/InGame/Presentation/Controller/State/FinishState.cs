using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.InGame.Presentation.View;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class FinishState : BaseState
    {
        private readonly CorkView _corkView;

        public FinishState(CorkView corkView)
        {
            _corkView = corkView;
        }

        public override GameState state => GameState.Finish;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await UniTask.Yield(token);

            // TODO: ユーザーの記録更新 + ランキング送信
            var score = _corkView.flyingDistance; // 飛距離がスコア

            // ランキング反映待ち
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            return GameState.Result;
        }
    }
}