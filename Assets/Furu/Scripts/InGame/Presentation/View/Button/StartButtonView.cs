using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;

namespace Furu.InGame.Presentation.View
{
    public sealed class StartButtonView : BaseButtonView
    {
        public async UniTask PushAsync(CancellationToken token)
        {
            await push.ToUniTask(true, token);
        }
    }
}