using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class TitleView : MonoBehaviour
    {
        [SerializeField] private StartButtonView startButton = default;

        [SerializeField] private TopView topView = default;

        public async UniTaskVoid SetUpAsync(float animationTime, CancellationToken token)
        {
            topView.SetUpAsync(animationTime, token).Forget();
            await UniTask.Yield(token);
        }

        public async UniTask StartAsync(float animationTime, CancellationToken token)
        {
            await startButton.PushAsync(token);

            await topView.HideAsync(animationTime, token);
        }
    }
}