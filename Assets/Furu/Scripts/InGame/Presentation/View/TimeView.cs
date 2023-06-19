using Furu.Base.Presentation.View;
using TMPro;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class TimeView : BaseView<float>
    {
        [SerializeField] private TextMeshProUGUI time = default;

        public override void Render(float value)
        {
            time.text = $"{value.ToString("F2")}";
        }
    }
}