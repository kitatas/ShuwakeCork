using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common;
using Furu.InGame.Presentation.View;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class TitleState : BaseState
    {
        private readonly TitleView _titleView;

        public TitleState(TitleView titleView)
        {
            _titleView = titleView;
        }

        public override GameState state => GameState.Title;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _titleView.SetUpAsync(UiConfig.POPUP_TIME, token).Forget();
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            await _titleView.StartAsync(UiConfig.POPUP_TIME, token);

            return GameState.SetUp;
        }
    }
}