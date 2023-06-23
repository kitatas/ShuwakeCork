using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;
using TMPro;
using UnityEngine;

namespace Furu.Common.Presentation.View
{
    public sealed class CrashView : BaseCanvasGroupView
    {
        [SerializeField] private TextMeshProUGUI messageText = default;
        [SerializeField] private ExceptionButtonView exceptionButton = default;

        public async UniTask ShowAndPushAsync(string message, float animationTime, CancellationToken token)
        {
            messageText.text = $"{message}";

            await ShowAsync(animationTime, token);

            await exceptionButton.PushAsync(token);
        }
    }
}