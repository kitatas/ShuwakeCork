using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class CorkView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer = default;
        [SerializeField] private Rigidbody2D rigidbody2d = default;

        public void Init()
        {
            // Hide
            spriteRenderer
                .DOFade(0.0f, 0.0f)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
            rigidbody2d.simulated = false;
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