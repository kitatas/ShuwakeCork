using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniEx;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class CorkView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer = default;
        [SerializeField] private Rigidbody2D rigidbody2d = default;

        public void Init(Func<GameState, bool> isState)
        {
            // Hide
            spriteRenderer
                .DOFade(0.0f, 0.0f)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
            rigidbody2d.simulated = false;

            this.UpdateAsObservable()
                .Where(_ =>
                {
                    var isBurstState = isState?.Invoke(GameState.Burst);
                    return isBurstState.HasValue && isBurstState.Value;
                })
                .Skip(1)
                .Select(_ => transform.position)
                .Pairwise()
                .Subscribe(x =>
                {
                    var direction = x.Current - x.Previous;
                    var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    // 位置の差分が小さいとき、angleが0になる
                    if (angle.IsZero()) return;

                    transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
                })
                .AddTo(this);
        }

        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            await spriteRenderer
                .DOFade(1.0f, animationTime)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        public void Shot(Vector2 power)
        {
            transform.SetParent(null);
            rigidbody2d.simulated = true;
            rigidbody2d.AddForce(power);
        }
    }
}