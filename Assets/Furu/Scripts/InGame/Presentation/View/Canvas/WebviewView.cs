using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class WebviewView : BaseCanvasGroupView
    {
        [SerializeField] private CanvasButtonView close = default;

        [SerializeField] private RectTransform panel = default;
        [SerializeField] private Canvas canvas = default;

        private WebViewObject _webViewObject = null;
        private (int left, int top, int right, int bottom) _margin;

        public async UniTaskVoid SetUpAsync(float animationTime, CancellationToken token)
        {
            close.pushed += () => HideAsync(animationTime, token).Forget();

            await HideAsync(0.0f, token);
        }

        private void Start()
        {
            _margin = GetMargins();
        }

        private (int, int, int, int) GetMargins()
        {
            var corners = new Vector3[4];
            panel.GetWorldCorners(corners);
            var screenCorner1 = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[1]);
            var screenCorner3 = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, corners[3]);
            var rect = new Rect
            {
                x = screenCorner1.x,
                y = screenCorner3.y,
                width = screenCorner3.x - screenCorner1.x,
                height = screenCorner1.y - screenCorner3.y,
            };

            return (
                (int)rect.xMin,
                Screen.height - (int)rect.yMax,
                Screen.width - (int)rect.xMax,
                (int)rect.yMin
            );
        }

        public async UniTask ShowAsync(string url, float animationTime, CancellationToken token)
        {
            _webViewObject = new GameObject("WebViewObject").AddComponent<WebViewObject>();
            _webViewObject.Init(enableWKWebView: true);

#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            _webViewObject.bitmapRefreshCycle = 1;
#endif
            _webViewObject.LoadURL(url);
            _webViewObject.SetMargins(_margin.left, _margin.top, _margin.right, _margin.bottom);

            await ShowAsync(animationTime, token);

            _webViewObject.SetVisibility(true);
        }

        public override async UniTask HideAsync(float animationTime, CancellationToken token)
        {
            if (_webViewObject != null)
            {
                _webViewObject.SetVisibility(false);
                Destroy(_webViewObject.gameObject);
            }

            await base.HideAsync(animationTime, token);
        }
    }
}