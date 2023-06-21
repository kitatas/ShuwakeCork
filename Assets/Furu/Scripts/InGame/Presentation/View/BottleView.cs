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
        [SerializeField] private CameraView cameraView = default;

        [SerializeField] private SpriteRenderer spriteRenderer = default;
        [SerializeField] private Collider2D collider2d = default;
        [SerializeField] private ParticleSystem splash = default;

        private Func<GameState, bool> _isState;
        private Subject<Vector3> _dragPosition;
        public float shakePower { get; private set; }

        public void Init(Func<GameState, bool> isState)
        {
            _isState = isState;
            _dragPosition = new Subject<Vector3>();
            shakePower = 0.0f;

            _dragPosition
                .Skip(1)
                .Pairwise()
                .Subscribe(x =>
                {
                    // Drag した距離を力に変換する
                    shakePower += x.Current.GetLength(x.Previous);
                })
                .AddTo(this);

            // Hide
            spriteRenderer
                .DOFade(0.0f, 0.0f)
                .SetEase(Ease.Linear)
                .SetLink(gameObject);
            collider2d.enabled = false;
            splash.Stop();
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
                var position = cameraView.GetWorldPoint(eventData.position);
                position
                    .ClampX(-2.0f, 2.0f)
                    .ClampY(-2.0f, 2.0f)
                    .SetZ(0.0f);
                transform.position = position;
                _dragPosition?.OnNext(position);
            }
        }

        public void ShowSplash()
        {
            splash.Play();
        }
    }
}