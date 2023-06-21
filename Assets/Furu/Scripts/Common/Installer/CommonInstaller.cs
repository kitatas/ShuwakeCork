using Furu.Common.Domain.UseCase;
using Furu.Common.Presentation.Presenter;
using VContainer;
using VContainer.Unity;

namespace Furu.Common.Installer
{
    public sealed class CommonInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<SceneUseCase>(Lifetime.Singleton);

            // Presenter
            builder.RegisterEntryPoint<ScenePresenter>();
        }
    }
}