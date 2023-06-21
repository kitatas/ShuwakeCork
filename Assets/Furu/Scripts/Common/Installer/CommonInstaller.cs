using Furu.Common.Data.Entity;
using Furu.Common.Domain.Repository;
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
            // Entity
            builder.Register<UserEntity>(Lifetime.Singleton);

            // Repository
            builder.Register<PlayFabRepository>(Lifetime.Singleton);
            builder.Register<SaveRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<SceneUseCase>(Lifetime.Singleton);

            // Presenter
            builder.RegisterEntryPoint<ScenePresenter>();
        }
    }
}