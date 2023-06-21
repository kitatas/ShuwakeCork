using Furu.Boot.Domain.UseCase;
using Furu.Boot.Presentation.Controller;
using Furu.Boot.Presentation.Presenter;
using VContainer;
using VContainer.Unity;

namespace Furu.Boot.Installer
{
    public sealed class BootInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<StateController>(Lifetime.Scoped);
            builder.Register<LoadState>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<StatePresenter>();
        }
    }
}