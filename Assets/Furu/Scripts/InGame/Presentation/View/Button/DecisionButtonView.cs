using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;
using UniRx;

namespace Furu.InGame.Presentation.View
{
    public sealed class DecisionButtonView : BaseButtonView
    {
        public IObservable<Unit> Decision()
        {
            return push;
        }

        public async UniTask PushAsync(CancellationToken token)
        {
            await push.ToUniTask(true, token);
        }
    }
}