using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class LiquidView : MonoBehaviour
    {
        [SerializeField] private Transform mask = default;
        [SerializeField] private SpriteRenderer fountain = default;
        [SerializeField] private SpriteRenderer liquid = default;
        [SerializeField] private SplashView splashView = default;

        public void Init()
        {
            splashView.Init();
        }
        
        public void SetUp(Data.Entity.LiquidEntity entity)
        {
            fountain.color = entity.color;
            liquid.color = entity.color;

            splashView.SetUp(entity);
        }

        public async UniTask PourAsync(float animationTime, CancellationToken token)
        {
            var time = animationTime / 3.0f;

            // 底に到達するまでの時間
            await fountain.transform
                .DOLocalMoveY(3.9f, time)
                .SetEase(Ease.Linear)
                .SetLink(fountain.gameObject);

            await DOTween.Sequence()
                .Append(mask
                    .DOLocalMoveY(2.0f, animationTime)
                    .SetEase(Ease.InCirc)
                    .SetLink(mask.gameObject))
                .Join(fountain.transform
                    .DOScaleY(1.3f, animationTime)
                    .SetEase(Ease.Linear)
                    .SetLink(fountain.gameObject))
                .Join(fountain.transform
                    .DOLocalMoveY(-0.2f, animationTime)
                    .SetEase(Ease.Linear)
                    .SetLink(fountain.gameObject))
                .WithCancellation(token);

            fountain.gameObject.SetActive(false);
        }

        public void Splash()
        {
            DOTween.Sequence()
                .Append(mask
                    .DOLocalMoveY(0.75f, 1.0f)
                    .SetEase(Ease.OutCirc)
                    .SetLink(mask.gameObject));

            splashView.Play();
        }
    }
}