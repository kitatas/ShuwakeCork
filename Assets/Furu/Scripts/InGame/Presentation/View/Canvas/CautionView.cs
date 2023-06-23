using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class CautionView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView close = default;

        public async UniTaskVoid SetUpAsync(float animationTime, CancellationToken token)
        {
            close.pushed += () => HideAsync(animationTime, token).Forget();

            await HideAsync(0.0f, token);
        }
    }
}