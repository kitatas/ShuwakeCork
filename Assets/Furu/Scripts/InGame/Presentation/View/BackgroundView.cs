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

        private readonly float _interval = 62.22f;

        private void Awake()
        {
            this.UpdateAsObservable()
                .Select(_ => cameraView.position)
                .Pairwise()
                .Subscribe(pair =>
                {
                    backgrounds.Each(x =>
                    {
                        var cameraX = pair.Current.x;
                        var currentX = x.position.x;
                        if (cameraX > pair.Previous.x)
                        {
                            // forward
                            if (cameraX > currentX + _interval)
                            {
                                x.SetLocalPositionX(currentX + _interval * 3);
                            }
                        }
                        else if (cameraX < pair.Previous.x)
                        {
                            // back
                            if (cameraX < currentX - _interval)
                            {
                                x.SetLocalPositionX(currentX - _interval * 3);
                            }
                        }

                        var cameraY = pair.Current.y;
                        var currentY = x.position.y;
                        if (cameraY > pair.Previous.y)
                        {
                            // up
                            if (cameraY > currentY + _interval)
                            {
                                x.SetLocalPositionY(currentY + _interval * 2);
                            }
                        }
                        else if (cameraY < pair.Previous.y)
                        {
                            // down
                            if (cameraY < currentY - _interval)
                            {
                                x.SetLocalPositionY(currentY - _interval * 2);
                            }
                        }
                    });
                })
                .AddTo(this);
        }
    }
}