using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;

namespace Furu.Common.Presentation.View
{
    public sealed class ExceptionButtonView : BaseButtonView
    {
        public async UniTask PushAsync(CancellationToken token)
        {
            await push.ToUniTask(true, token);
        }
    }
}