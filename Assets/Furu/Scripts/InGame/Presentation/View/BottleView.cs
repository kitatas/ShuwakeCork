using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Furu.Common;
using UniEx;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Furu.InGame.Presentation.View
{
    public sealed class BottleView : MonoBehaviour, IDragHandler
    {
        [SerializeField] private CameraView cameraView = default;

        private Func<GameState, bool> _isState;
        private Action<SeType> _playSe;
        private Subject<Vector3> _dragPosition;
        private int _dragFrame;
        public float shakePower { get; private set; }

        public void Init(Func<GameState, bool> isState, Action<SeType> playSe)
        {
            _isState = isState;
            _playSe = playSe;
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
        }

        public async UniTask VibrateAsync(float animationTime, CancellationToken token)
        {
            await DOTween.Sequence()
                .Append(transform
                    .DOLocalMoveX(-0.1f, animationTime))
                .Append(transform
                    .DOLocalMoveX(0.1f, animationTime))
                .Append(transform
                    .DOLocalMoveX(-0.1f, animationTime))
                .Append(transform
                    .DOLocalMoveX(0.0f, animationTime))
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        public async UniTask RotateAsync(float animationTime, CancellationToken token)
        {
            await transform
                .DOLocalRotate(new Vector3(0.0f, 0.0f, -45.0f), animationTime)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var isState = _isState?.Invoke(GameState.Input);
            if (isState.HasValue && isState.Value)
            {
                var position = cameraView.GetWorldPoint(eventData.position);
                position.SetZ(0.0f);
                transform.position = position;
                _dragPosition?.OnNext(position);

                if (_dragFrame.IsEqual(0))
                {
                    _playSe?.Invoke(SeType.Shake);
                }
                _dragFrame.RepeatIncrement(0, 10);
            }
        }

        public async UniTask ResetPositionAsync(float animationTime, CancellationToken token)
        {
            await transform
                .DOLocalMove(Vector3.zero, animationTime)
                .SetEase(Ease.Linear)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        public void SyncAngle(float z)
        {
            transform.SetEulerAngleZ(z);
        }
    }
}