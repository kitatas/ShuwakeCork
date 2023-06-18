using Furu.InGame.Domain.UseCase;
using Furu.InGame.Presentation.Controller;
using Furu.InGame.Presentation.Presenter;
using VContainer;
using VContainer.Unity;

namespace Furu.InGame.Installer
{
    public sealed class InGameInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<StateController>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<StatePresenter>();
        }
    }
}