using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;
using Furu.Common;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class InformationView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView credit = default;
        [SerializeField] private CanvasButtonView license = default;
        [SerializeField] private CanvasButtonView policy = default;
        [SerializeField] private DecisionButtonView otherApp = default;
        
        [SerializeField] private CanvasButtonView close = default;

        [SerializeField] private WebviewView webviewView = default;

        public Action hide;

        public async UniTaskVoid SetUpAsync(float animationTime, CancellationToken token)
        {
            credit.pushed += () => webviewView.ShowAsync(UrlConfig.CREDIT, animationTime, token).Forget();
            license.pushed += () => webviewView.ShowAsync(UrlConfig.LICENSE, animationTime, token).Forget();
            policy.pushed += () => webviewView.ShowAsync(UrlConfig.POLICY, animationTime, token).Forget();
            otherApp.pushed += () => Application.OpenURL(UrlConfig.DEVELOPER_APP);

            close.pushed += () =>
            {
                hide?.Invoke();
                HideAsync(animationTime, token).Forget();
            };

            webviewView.SetUpAsync(animationTime, token).Forget();
            await HideAsync(0.0f, token);
        }
    }
}