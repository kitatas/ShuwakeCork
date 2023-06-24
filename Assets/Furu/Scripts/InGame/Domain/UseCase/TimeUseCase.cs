using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Furu.Base.Domain.UseCase;
using UniEx;
using UnityEngine;

namespace Furu.InGame.Domain.UseCase
{
    public sealed class TimeUseCase : BaseModelUseCase<float>
    {
        public TimeUseCase()
        {
            Set(0.0f);
        }

        public async UniTask IncreaseAsync(float animationTime, CancellationToken token)
        {
            await DOTween.To(
                    () => property.Value,
                    x => Set(x),
                    1.0f,
                    animationTime)
                .SetEase(Ease.OutQuart)
                .WithCancellation(token);
        }

        public void Decrease(float deltaTime, float fullTime)
        {
            var time = property.Value - (deltaTime / fullTime);
            Set(Mathf.Max(time, 0.0f));
        }

        public bool IsTimeUp()
        {
            return property.Value.IsZero();
        }
    }
}