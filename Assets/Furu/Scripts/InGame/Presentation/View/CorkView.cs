using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Furu.Common;
using UniEx;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class CorkView : MonoBehaviour
    {
        [SerializeField] private CameraView cameraView = default;

        [SerializeField] private Rigidbody2D rigidbody2d = default;
        [SerializeField] private TrailRenderer trailRenderer = default;

        private bool _isGround;
        public float height { get; private set; } = 0.0f;
        public float flyingDistance => transform.position.x;
        public float currentHeight => transform.position.y;

        public void Init(Func<GameState, bool> isState, Action<SeType> playSe)
        {
            // Hide
            rigidbody2d.simulated = false;
            trailRenderer.enabled = false;

            this.OnCollisionEnter2DAsObservable()
                .Where(x => x.gameObject.CompareTag(TagConfig.GROUND))
                .Subscribe(_ =>
                {
                    _isGround = true;
                    playSe?.Invoke(SeType.Ground);
                })
                .AddTo(this);

            this.UpdateAsObservable()
                .Where(_ => _isGround == false) // 一度接地したら角度の計算をしない
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
                    if (height < x.Current.y)
                    {
                        height = x.Current.y;
                    }

                    var direction = x.Current - x.Previous;
                    var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    // 位置の差分が小さいとき、angleが0になる
                    if (angle.IsZero()) return;

                    transform.eulerAngles = new Vector3(0.0f, 0.0f, angle);
                })
                .AddTo(this);

            this.LateUpdateAsObservable()
                .Where(_ =>
                {
                    var isBurstState = isState?.Invoke(GameState.Burst);
                    return isBurstState.HasValue && isBurstState.Value;
                })
                .Subscribe(_ => cameraView.Chase(flyingDistance, currentHeight))
                .AddTo(this);
        }

        public async UniTask CloseAsync(float closeHeight, float animationTime, CancellationToken token)
        {
            await transform
                .DOLocalMoveY(closeHeight, animationTime)
                .SetEase(Ease.InCirc)
                .SetLink(gameObject)
                .WithCancellation(token);
        }

        public void Shot(Vector2 power)
        {
            transform.SetParent(null);
            rigidbody2d.simulated = true;
            rigidbody2d.AddForce(power);
            trailRenderer.enabled = true;
        }

        public bool IsStop()
        {
            // 一度も接地していない
            if (_isGround == false) return false;

            if (rigidbody2d.velocity.magnitude <= 0.05f)
            {
                rigidbody2d.velocity = Vector2.zero;
                return true;
            }

            return false;
        }
    }
}