using Furu.Common.Data.DataStore;
using Furu.Common.Data.Entity;
using Furu.Common.Domain.Repository;
using Furu.Common.Domain.UseCase;
using Furu.Common.Presentation.Controller;
using Furu.Common.Presentation.Presenter;
using Furu.Common.Presentation.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Furu.Common.Installer
{
    public sealed class CommonInstaller : LifetimeScope
    {
        [SerializeField] private BgmTable bgmTable = default;
        [SerializeField] private SeTable seTable = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<BgmTable>(bgmTable);
            builder.RegisterInstance<SeTable>(seTable);

            // Entity
            builder.Register<UserEntity>(Lifetime.Singleton);

            // Repository
            builder.Register<PlayFabRepository>(Lifetime.Singleton);
            builder.Register<SaveRepository>(Lifetime.Singleton);
            builder.Register<SoundRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<LoadingUseCase>(Lifetime.Singleton);
            builder.Register<SceneUseCase>(Lifetime.Singleton);
            builder.Register<SoundUseCase>(Lifetime.Singleton);

            // Controller
            builder.Register<ExceptionController>(Lifetime.Singleton);

            // Presenter
            builder.RegisterEntryPoint<LoadingPresenter>();
            builder.RegisterEntryPoint<ScenePresenter>();
            builder.RegisterEntryPoint<SoundPresenter>();

            // View
            builder.RegisterInstance<CrashView>(FindObjectOfType<CrashView>());
            builder.RegisterInstance<LoadingView>(FindObjectOfType<LoadingView>());
            builder.RegisterInstance<RetryView>(FindObjectOfType<RetryView>());
            builder.RegisterInstance<RebootView>(FindObjectOfType<RebootView>());
            builder.RegisterInstance<SoundView>(FindObjectOfType<SoundView>());
            builder.RegisterInstance<TransitionView>(FindObjectOfType<TransitionView>());
        }
    }
}