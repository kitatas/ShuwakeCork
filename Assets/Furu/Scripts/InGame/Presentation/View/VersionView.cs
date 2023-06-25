using Furu.Common;
using TMPro;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class VersionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI version = default;

        private void Awake()
        {
            version.text = $"ver: {AppConfig.APP_VERSION}";
        }
    }
}