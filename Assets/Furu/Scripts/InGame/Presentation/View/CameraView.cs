using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class CameraView : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera = default;

        public Vector3 GetWorldPoint(Vector3 cursorPosition)
        {
            return mainCamera.ScreenToWorldPoint(cursorPosition);
        }

        public void Chase(float x)
        {
            transform.position = new Vector3(x, 0.0f, -10.0f);
        }
    }
}