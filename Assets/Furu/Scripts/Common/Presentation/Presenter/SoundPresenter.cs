using Furu.Common.Domain.UseCase;
using Furu.Common.Presentation.View;
using UniEx;
using UniRx;
using VContainer.Unity;

namespace Furu.Common.Presentation.Presenter
{
    public sealed class SoundPresenter : IInitializable, IStartable
    {
        private readonly SoundUseCase _soundUseCase;
        private readonly SoundView _soundView;

        public SoundPresenter(SoundUseCase soundUseCase, SoundView soundView)
        {
            _soundUseCase = soundUseCase;
            _soundView = soundView;
        }

        public void Initialize()
        {
            _soundUseCase.playBgm
                .Subscribe(_soundView.PlayBgm)
                .AddTo(_soundView);

            _soundUseCase.playSe
                .Subscribe(x =>
                {
                    // 遅延を考慮した再生
                    _soundView.Delay(x.delay, () => _soundView.PlaySe(x.clip));
                })
                .AddTo(_soundView);

            _soundUseCase.bgmVolume
                .Subscribe(_soundView.SetBgmVolume)
                .AddTo(_soundView);

            _soundUseCase.seVolume
                .Subscribe(_soundView.SetSeVolume)
                .AddTo(_soundView);
        }

        public void Start()
        {
            _soundUseCase.PlayBgm(BgmType.Title);
        }
    }
}