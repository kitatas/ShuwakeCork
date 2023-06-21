using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;

namespace Furu.Boot.Presentation.View
{
    public sealed class DecisionButtonView : BaseButtonView
    {
        public async UniTask PushAsync(CancellationToken token)
        {
            await push.ToUniTask(true, token);
        }
    }
}