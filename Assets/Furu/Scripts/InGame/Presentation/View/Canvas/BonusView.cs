using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Furu.Base.Presentation.View;
using TMPro;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class BonusView : BaseCanvasGroupView
    {
        [SerializeField] private TextMeshProUGUI title = default;
        [SerializeField] private RectTransform highlight = default;

        [SerializeField] private DecisionButtonView decision = default;

        private void Awake()
        {
            title.text = $"{GameConfig.PLAY_BONUS}回プレイボーナス";

            var endValue = new Vector3(0.0f, 0.0f, -360.0f);
            highlight.DORotate(endValue, 2.5f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1)
                .SetLink(gameObject);
        }

        public async UniTask PopAsync(float animationTime, CancellationToken token)
        {
            await ShowAsync(animationTime, token);

            await decision.PushAsync(token);
        }
    }
}