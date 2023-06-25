using System;
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

        private readonly float _interval = 49.77f;

        private void Start()
        {
            TraceForward();
            TraceBack();
        }

        private void TraceForward()
        {
            var index = 0;
            TraceAsObservable()
                .Where(x => x.Current > x.Previous)
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

        private void TraceBack()
        {
            var index = backgrounds.GetLastIndex();
            TraceAsObservable()
                .Where(x => x.Current < x.Previous)
                .Subscribe(_ =>
                {
                    var x = backgrounds[index].position.x;
                    if (cameraView.x <= x - _interval)
                    {
                        backgrounds[index].SetLocalPositionX(x - _interval * backgrounds.Length);
                        index.RepeatDecrement(0, backgrounds.GetLastIndex());
                    }
                })
                .AddTo(this);
        }

        private IObservable<Pair<float>> TraceAsObservable()
        {
            return this.UpdateAsObservable()
                .Select(_ => cameraView.x)
                .Pairwise();
        }
    }
}