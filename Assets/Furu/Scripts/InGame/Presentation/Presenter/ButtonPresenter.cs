using Furu.Base.Presentation.View;
using Furu.Common.Domain.UseCase;
using Furu.InGame.Presentation.View;
using UniRx;
using UnityEngine;
using VContainer.Unity;

namespace Furu.InGame.Presentation.Presenter
{
    public sealed class ButtonPresenter : IInitializable
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly VolumeView _volumeView;

        public ButtonPresenter(SoundUseCase soundUseCase, VolumeView volumeView)
        {
            _soundUseCase = soundUseCase;
            _volumeView = volumeView;
        }

        public void Initialize()
        {
            foreach (var buttonView in Object.FindObjectsOfType<BaseButtonView>())
            {
                buttonView.Init();
            }

            _volumeView.Init(_soundUseCase.bgmVolumeValue, _soundUseCase.seVolumeValue);

            _volumeView.updateBgmVolume
                .Subscribe(_soundUseCase.SetBgmVolume)
                .AddTo(_volumeView);

            _volumeView.updateSeVolume
                .Subscribe(_soundUseCase.SetSeVolume)
                .AddTo(_volumeView);

            _volumeView.releaseVolume
                .Subscribe(_soundUseCase.PlaySe)
                .AddTo(_volumeView);
        }
    }
}