using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniEx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Furu.InGame.Presentation.View
{
    public sealed class ArrowView : MonoBehaviour, IDragHandler
    {
        [SerializeField] private CameraView cameraView = default;
        [SerializeField] private SpriteRenderer[] sprites = default;
        [SerializeField] private Collider2D collider2d = default;

        private readonly float _radius = 2.5f;

        private Func<GameState, bool> _isState;

        public async UniTaskVoid InitAsync(Func<GameState, bool> isState, CancellationToken token)
        {
            _isState = isState;
            await HideAsync(0.0f, token);
        }

        public async UniTask ShowAsync(float animationTime, CancellationToken token)
        {
            var tasks = new List<UniTask>();
            foreach (var sprite in sprites)
            {
                var task = sprite
                    .DOFade(1.0f, animationTime)
                    .SetEase(Ease.Linear)
                    .SetLink(gameObject)
                    .WithCancellation(token);
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);

            collider2d.enabled = true;
        }

        public async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            collider2d.enabled = false;

            var tasks = new List<UniTask>();
            foreach (var sprite in sprites)
            {
                var task = sprite
                    .DOFade(0.0f, animationTime)
                    .SetEase(Ease.Linear)
                    .SetLink(gameObject)
                    .WithCancellation(token);
                tasks.Add(task);
            }

            await UniTask.WhenAll(tasks);
        }

        public void OnDrag(PointerEventData eventData)
        {
            var isState = _isState?.Invoke(GameState.Angle);
            if (isState.HasValue && isState.Value)
            {
                // Bottle の円周上
                var position = cameraView.GetWorldPoint(eventData.position);
                position.SetZ(0.0f);
                transform.position = position.normalized * _radius;

                // angle
                var angle = Mathf.Atan2(position.y, position.x) * Mathf.Rad2Deg;
                transform.eulerAngles = new Vector3(0.0f, 0.0f, angle + -90.0f);
            }
        }

        public float angleZ => transform.eulerAngles.z;

        public Vector3 direction => transform.position.normalized;
    }
}