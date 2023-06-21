using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Furu.Base.Presentation.View;
using TMPro;

namespace Furu.InGame.Presentation.View
{
    public sealed class ReloadButtonView : BaseButtonView
    {
        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(image
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.Linear))
                .Join(image.GetComponentInChildren<TextMeshProUGUI>()
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.Linear))
                .SetLink(gameObject)
                .WithCancellation(token);

            Activate(true);
        }

        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            Activate(false);

            await DOTween.Sequence()
                .Append(image
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.Linear))
                .Join(image.GetComponentInChildren<TextMeshProUGUI>()
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.Linear))
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        public async UniTask PushAsync(CancellationToken token)
        {
            await push.ToUniTask(true, token);
        }
    }
}