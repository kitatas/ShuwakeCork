using Furu.Base.Domain.UseCase;
using Furu.Base.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace Furu.Base.Presentation.Presenter
{
    public abstract class BasePresenter<T> : IInitializable where T : new()
    {
        private readonly BaseModelUseCase<T> _modelUseCase;
        private readonly BaseView<T> _view;

        public BasePresenter(BaseModelUseCase<T> modelUseCase, BaseView<T> view)
        {
            _modelUseCase = modelUseCase;
            _view = view;
        }

        public virtual void Initialize()
        {
            _modelUseCase.property
                .Subscribe(_view.Render)
                .AddTo(_view);
        }
    }
}