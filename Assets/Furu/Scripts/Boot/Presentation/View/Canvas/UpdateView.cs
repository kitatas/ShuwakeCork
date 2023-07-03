using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;
using Furu.Common;
using UnityEngine;

namespace Furu.Boot.Presentation.View
{
    public sealed class UpdateView : BaseCanvasGroupView
    {
        [SerializeField] private DecisionButtonView decision = default;

        public async UniTaskVoid InitAsync(CancellationToken token)
        {
            decision.pushed += () => Application.OpenURL(UrlConfig.APP);

            HideAsync(0.0f, token).Forget();
            await UniTask.Yield(token);
        }
    }
}