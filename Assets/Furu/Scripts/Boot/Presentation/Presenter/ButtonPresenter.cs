using Furu.Base.Presentation.View;
using UnityEngine;
using VContainer.Unity;

namespace Furu.Boot.Presentation.Presenter
{
    public sealed class ButtonPresenter : IInitializable
    {
        public void Initialize()
        {
            foreach (var buttonView in Object.FindObjectsOfType<BaseButtonView>())
            {
                buttonView.Init();
            }
        }
    }
}