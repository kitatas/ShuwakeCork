using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class TitleView : MonoBehaviour
    {
        [SerializeField] private StartButtonView startButton = default;

        public async UniTask StartAsync(CancellationToken token)
        {
            await startButton.PushAsync(token);
        }
    }
}