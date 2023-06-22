using Furu.Boot.Domain.UseCase;
using Furu.Boot.Presentation.Controller;
using Furu.Boot.Presentation.Presenter;
using Furu.Boot.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Furu.Boot.Installer
{
    public sealed class BootInstaller : LifetimeScope
    {
        [SerializeField] private RegisterView registerView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<LoginUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<StateController>(Lifetime.Scoped);
            builder.Register<CheckState>(Lifetime.Scoped);
            builder.Register<LoadState>(Lifetime.Scoped);
            builder.Register<LoginState>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<StatePresenter>();

            // View
            builder.RegisterInstance<RegisterView>(registerView);
        }
    }
}