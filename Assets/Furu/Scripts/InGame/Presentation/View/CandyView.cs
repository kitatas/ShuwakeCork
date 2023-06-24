using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class CandyView : MonoBehaviour
    {
        [SerializeField] private Transform body = default;

        private void Awake()
        {
            var endValue = new Vector3(0.0f, 0.0f, -360.0f);
            body.DORotate(endValue, 1.0f, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1)
                .SetLink(gameObject);
        }

        public void Activate(bool value)
        {
            body.gameObject.SetActive(value);
        }

        public async UniTask ThrowAsync(float animationTime, CancellationToken token)
        {
            await body
                .DOLocalMoveY(-1.0f, animationTime)
                .SetEase(Ease.InCirc)
                .SetLink(body.gameObject)
                .WithCancellation(token);
        }
    }
}