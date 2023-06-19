using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Furu.InGame.Presentation.View
{
    public sealed class BottleView : MonoBehaviour, IDragHandler
    {
        [SerializeField] private Camera mainCamera = default;
        [SerializeField] private SpriteRenderer spriteRenderer = default;
        [SerializeField] private Collider2D collider2d = default;

        private Func<GameState, bool> _isState;

        public void Init(Func<GameState, bool> isState)
        {
            _isState = isState;

            // Hide
            spriteRenderer
                .DOFade(0.0f, 0.0f)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
            collider2d.enabled = false;
        }

        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            await spriteRenderer
                .DOFade(1.0f, animationTime)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);

            collider2d.enabled = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var isState = _isState?.Invoke(GameState.Input);
            if (isState.HasValue && isState.Value)
            {
                var position = mainCamera.ScreenToWorldPoint(eventData.position);
                position.z = 0;
                transform.position = position;
            }
        }
    }
}