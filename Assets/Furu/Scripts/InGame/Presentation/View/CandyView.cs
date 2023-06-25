using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniEx;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class CandyView : MonoBehaviour
    {
        [SerializeField] private Transform[] candies = default;

        private void Awake()
        {
            var endValue = new Vector3(0.0f, 0.0f, -360.0f);
            for (int i = 0; i < candies.Length; i++)
            {
                candies[i]
                    .DORotate(endValue, 1.0f, RotateMode.FastBeyond360)
                    .SetDelay(Random.Range(0.0f, 0.5f))
                    .SetEase(Ease.Linear)
                    .SetLoops(-1)
                    .SetLink(gameObject);
            }
        }

        public void Activate(bool value)
        {
            candies.Each(x => x.gameObject.SetActive(value));
        }

        public async UniTask ThrowAsync(float animationTime, CancellationToken token)
        {
            var tasks = new List<UniTask>();
            for (int i = 0; i < candies.Length; i++)
            {
                tasks.Add(candies[i]
                    .DOLocalMoveY(-1.0f, animationTime)
                    .SetDelay(i * 0.04f)
                    .SetEase(Ease.InCirc)
                    .SetLink(candies[i].gameObject)
                    .WithCancellation(token));
            }

            await UniTask.WhenAll(tasks);
        }
    }
}