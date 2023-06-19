using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class CorkView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D rigidbody2d = default;

        public void Shot(Vector2 power)
        {
            transform.SetParent(null);
            rigidbody2d.simulated = true;
            rigidbody2d.AddForce(power);
        }
    }
}