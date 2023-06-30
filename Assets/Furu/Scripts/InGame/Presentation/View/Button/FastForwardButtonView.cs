using DG.Tweening;
using Furu.Base.Presentation.View;
using UnityEngine;
using UnityEngine.UI;

namespace Furu.InGame.Presentation.View
{
    public sealed class FastForwardButtonView : BaseButtonView
    {
        [SerializeField] private Image icon = default;

        [SerializeField] private Sprite normal = default;
        [SerializeField] private Sprite fast = default;

        public void Show(float animationTime)
        {
            DOTween.Sequence()
                .Append(image
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.Linear))
                .Join(image.GetComponentInChildren<Image>()
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.Linear))
                .SetLink(gameObject)
                .OnComplete(() => Activate(true));
        }

        public void Hide(float animationTime)
        {
            Activate(false);

            DOTween.Sequence()
                .Append(image
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.Linear))
                .Join(image.GetComponentInChildren<Image>()
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.Linear))
                .SetLink(gameObject);
        }

        public void SetIcon(bool value)
        {
            icon.sprite = value ? fast : normal;
        }
    }
}