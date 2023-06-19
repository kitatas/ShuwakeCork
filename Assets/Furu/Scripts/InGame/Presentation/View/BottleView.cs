using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniEx;
using UniRx;
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
        private Subject<Vector2> _dragPosition;
        public float dragPower { get; private set; }

        public void Init(Func<GameState, bool> isState)
        {
            _isState = isState;
            _dragPosition = new Subject<Vector2>();
            dragPower = 0.0f;

            _dragPosition
                .Skip(1)
                .Pairwise()
                .Subscribe(x =>
                {
                    // Drag した距離を力に変換する
                    dragPower += x.Current.GetSqrLength(x.Previous);
                })
                .AddTo(this);

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
                .SetLink(gameObject)
                .WithCancellation(token);

            collider2d.enabled = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var isState = _isState?.Invoke(GameState.Input);
            if (isState.HasValue && isState.Value)
            {
                var currentPosition = eventData.position;
                _dragPosition?.OnNext(currentPosition);

                var position = mainCamera.ScreenToWorldPoint(currentPosition);
                position.z = 0;
                transform.position = position;
            }
        }
    }
}