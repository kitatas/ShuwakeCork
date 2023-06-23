using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class TopView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView configButton = default;
        [SerializeField] private CanvasButtonView informationButton = default;

        [SerializeField] private ConfigView configView = default;
        [SerializeField] private InformationView informationView = default;

        public async UniTaskVoid SetUpAsync(float animationTime, CancellationToken token)
        {
            configButton.pushed += () =>
            {
                HideAsync(animationTime, token).Forget();
                configView.ShowAsync(animationTime, token).Forget();
            };
            configView.hide += () => ShowAsync(animationTime, token).Forget();

            informationButton.pushed += () =>
            {
                HideAsync(animationTime, token).Forget();
                informationView.ShowAsync(animationTime, token).Forget();
            };
            informationView.hide += () => ShowAsync(animationTime, token).Forget();

            configView.SetUpAsync(animationTime, token).Forget();
            informationView.SetUpAsync(animationTime, token).Forget();
            await ShowAsync(0.0f, token);
        }
    }
}