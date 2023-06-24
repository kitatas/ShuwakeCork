using Furu.Common.Domain.UseCase;
using Furu.Common.Presentation.View;
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
        }

        public void Start()
        {
            _soundUseCase.PlayBgm(BgmType.Title);
        }
    }
}