using UniEx;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class BackgroundView : MonoBehaviour
    {
        [SerializeField] private CameraView cameraView = default;
        [SerializeField] private RectTransform[] backgrounds = default;

        private readonly float _interval = 17.42f;

        private void Start()
        {
            var index = 0;
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    var x = backgrounds[index].position.x;
                    if (cameraView.x >= x + _interval)
                    {
                        backgrounds[index].SetLocalPositionX(x + _interval * backgrounds.Length);
                        index.RepeatIncrement(0, backgrounds.GetLastIndex());
                    }
                })
                .AddTo(this);
        }
    }
}