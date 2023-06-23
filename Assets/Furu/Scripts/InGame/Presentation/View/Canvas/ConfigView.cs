using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class ConfigView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView close = default;
        [SerializeField] private CanvasButtonView delete = default;

        [SerializeField] private CautionView cautionView = default;

        public Action hide;

        public async UniTaskVoid SetUpAsync(float animationTime, CancellationToken token)
        {
            close.pushed += () =>
            {
                hide?.Invoke();
                HideAsync(animationTime, token).Forget();
            };
            delete.pushed += () => cautionView.ShowAsync(animationTime, token).Forget();

            cautionView.SetUpAsync(animationTime, token).Forget();
            await HideAsync(0.0f, token);
        }
    }
}