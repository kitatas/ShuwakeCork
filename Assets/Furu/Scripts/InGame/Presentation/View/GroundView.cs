using UniEx;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class GroundView : MonoBehaviour
    {
        [SerializeField] private CameraView cameraView = default;
        [SerializeField] private Transform ground = default;

        private void Awake()
        {
            this.UpdateAsObservable()
                .Subscribe(_ => ground.SetPositionX(cameraView.position.x))
                .AddTo(this);
        }
    }
}