using UnityEngine;
using UnityEngine.UI;

namespace Furu.InGame.Presentation.View
{
    public sealed class CameraView : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera = default;
        [SerializeField] private CanvasScaler canvasScaler = default;

        private void Awake()
        {
            var resolution = canvasScaler.referenceResolution;
            var r = resolution.y / resolution.x;
            var s = (float)Screen.height / (float)Screen.width;
            var d = s / r;

            if (d > 1.0f)
            {
                mainCamera.orthographicSize *= d;
            }
        }

        public Vector3 GetWorldPoint(Vector3 cursorPosition)
        {
            return mainCamera.ScreenToWorldPoint(cursorPosition);
        }

        public void Chase(float x)
        {
            transform.position = new Vector3(x, 0.0f, -10.0f);
        }

        public float x => transform.position.x;
    }
}