using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class InformationView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView close = default;

        public Action hide;

        public async UniTaskVoid SetUpAsync(float animationTime, CancellationToken token)
        {
            close.pushed += () =>
            {
                hide?.Invoke();
                HideAsync(animationTime, token).Forget();
            };

            await HideAsync(0.0f, token);
        }
    }
}