using Furu.Base.Presentation.View;
using Furu.Common.Domain.UseCase;
using UnityEngine;
using VContainer.Unity;

namespace Furu.Boot.Presentation.Presenter
{
    public sealed class ButtonPresenter : IInitializable
    {
        private readonly SoundUseCase _soundUseCase;

        public ButtonPresenter(SoundUseCase soundUseCase)
        {
            _soundUseCase = soundUseCase;
        }

        public void Initialize()
        {
            foreach (var buttonView in Object.FindObjectsOfType<BaseButtonView>())
            {
                buttonView.Init(_soundUseCase.PlaySe);
            }
        }
    }
}