using Furu.Base.Presentation.View;
using UnityEngine;
using UnityEngine.UI;

namespace Furu.InGame.Presentation.View
{
    public sealed class TimeView : BaseView<float>
    {
        [SerializeField] private Image image = default;

        public override void Render(float value)
        {
            image.fillAmount = value;
        }
    }
}