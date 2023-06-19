using Furu.InGame.Domain.UseCase;
using Furu.InGame.Presentation.Controller;
using Furu.InGame.Presentation.Presenter;
using Furu.InGame.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Furu.InGame.Installer
{
    public sealed class InGameInstaller : LifetimeScope
    {
        [SerializeField] private BottleView bottleView = default;
        [SerializeField] private TitleView titleView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // UseCase
            builder.Register<StateUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<StateController>(Lifetime.Scoped);
            builder.Register<SetUpState>(Lifetime.Scoped);
            builder.Register<TitleState>(Lifetime.Scoped);

            // Presenter
            builder.RegisterEntryPoint<StatePresenter>();

            // View
            builder.RegisterInstance<BottleView>(bottleView);
            builder.RegisterInstance<TitleView>(titleView);
        }
    }
}