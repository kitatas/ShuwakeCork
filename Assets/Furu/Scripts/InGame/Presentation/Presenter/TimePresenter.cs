using Furu.Base.Presentation.Presenter;
using Furu.InGame.Domain.UseCase;
using Furu.InGame.Presentation.View;

namespace Furu.InGame.Presentation.Presenter
{
    public sealed class TimePresenter : BasePresenter<float>
    {
        public TimePresenter(TimeUseCase timeUseCase, TimeView timeView) : base(timeUseCase, timeView)
        {
        }
    }
}