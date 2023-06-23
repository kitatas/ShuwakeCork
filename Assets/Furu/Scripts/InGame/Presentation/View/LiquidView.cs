using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class LiquidView : MonoBehaviour
    {
        [SerializeField] private Transform mask = default;
        [SerializeField] private Transform liquid = default;

        public async UniTask PourAsync(float animationTime, CancellationToken token)
        {
            var time = animationTime / 3.0f;

            // 底に到達するまでの時間
            await liquid
                .DOLocalMoveY(3.9f, time)
                .SetEase(Ease.Linear)
                .SetLink(liquid.gameObject);

            await DOTween.Sequence()
                .Append(mask
                    .DOLocalMoveY(2.0f, animationTime)
                    .SetEase(Ease.InCirc)
                    .SetLink(mask.gameObject))
                .Join(liquid
                    .DOScaleY(1.3f, animationTime)
                    .SetEase(Ease.Linear)
                    .SetLink(liquid.gameObject))
                .Join(liquid
                    .DOLocalMoveY(-0.2f, animationTime)
                    .SetEase(Ease.Linear)
                    .SetLink(liquid.gameObject))
                .WithCancellation(token);

            liquid.gameObject.SetActive(false);
        }
    }
}