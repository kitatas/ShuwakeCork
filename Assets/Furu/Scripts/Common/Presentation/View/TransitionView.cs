using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using MPUIKIT;
using UnityEngine;
using UnityEngine.UI;

namespace Furu.Common.Presentation.View
{
    public sealed class TransitionView : MonoBehaviour
    {
        [SerializeField] private MPImage mask = default;
        [SerializeField] private Image raycastBlocker = default;

        private readonly float _minStrokeWidth = 1.0f;
        private readonly float _maxStrokeWidth = 560.0f;
        private static readonly int _strokeWidth = Shader.PropertyToID("_StrokeWidth");

        public async UniTask FadeInAsync(float animationTime, CancellationToken token)
        {
            raycastBlocker.raycastTarget = true;

            await DOTween.To(
                    () => mask.material.GetFloat(_strokeWidth),
                    x => mask.material.SetFloat(_strokeWidth, x),
                    _maxStrokeWidth,
                    animationTime)
                .SetEase(Ease.OutQuart)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        public async UniTask FadeOutAsync(float animationTime, CancellationToken token)
        {
            await DOTween.To(
                    () => mask.material.GetFloat(_strokeWidth),
                    x => mask.material.SetFloat(_strokeWidth, x),
                    _minStrokeWidth,
                    animationTime)
                .SetEase(Ease.InQuart)
                .SetLink(gameObject)
                .WithCancellation(token);

            raycastBlocker.raycastTarget = false;
        }
    }
}