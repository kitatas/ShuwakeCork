using Furu.InGame.Domain.Repository;
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
        [SerializeField] private TextAsset liquidMaster = default;

        [SerializeField] private BonusView bonusView = default;
        [SerializeField] private RankingView rankingView = default;
        [SerializeField] private AccountDeleteView accountDeleteView = default;
        [SerializeField] private ArrowView arrowView = default;
        [SerializeField] private BottleView bottleView = default;
        [SerializeField] private CandyView candyView = default;
        [SerializeField] private CorkView corkView = default;
        [SerializeField] private LiquidView liquidView = default;
        [SerializeField] private NameInputView nameInputView;
        [SerializeField] private TimeView timeView = default;
        [SerializeField] private TitleView titleView = default;
        [SerializeField] private UserRecordView userRecordView = default;
        [SerializeField] private VolumeView volumeView = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // Repository
            builder.Register<LiquidRepository>(Lifetime.Scoped).WithParameter(liquidMaster);

            // UseCase
            builder.Register<LiquidUseCase>(Lifetime.Scoped);
            builder.Register<RankingUseCase>(Lifetime.Scoped);
            builder.Register<StateUseCase>(Lifetime.Scoped);
            builder.Register<TimeUseCase>(Lifetime.Scoped);
            builder.Register<UserDataUseCase>(Lifetime.Scoped);
            builder.Register<UserRecordUseCase>(Lifetime.Scoped);

            // Controller
            builder.Register<StateController>(Lifetime.Scoped);
            builder.Register<AngleState>(Lifetime.Scoped);
            builder.Register<BurstState>(Lifetime.Scoped);
            builder.Register<FinishState>(Lifetime.Scoped);
            builder.Register<InputState>(Lifetime.Scoped);
            builder.Register<ResultState>(Lifetime.Scoped);
            builder.Register<SetUpState>(Lifetime.Scoped);
            builder.Register<TitleState>(Lifetime.Scoped);

            // Controller
            builder.RegisterEntryPoint<UserDataController>();

            // Presenter
            builder.RegisterEntryPoint<ButtonPresenter>();
            builder.RegisterEntryPoint<StatePresenter>();
            builder.RegisterEntryPoint<TimePresenter>();

            // View
            builder.RegisterInstance<BonusView>(bonusView);
            builder.RegisterInstance<RankingView>(rankingView);
            builder.RegisterInstance<AccountDeleteView>(accountDeleteView);
            builder.RegisterInstance<ArrowView>(arrowView);
            builder.RegisterInstance<BottleView>(bottleView);
            builder.RegisterInstance<CandyView>(candyView);
            builder.RegisterInstance<CorkView>(corkView);
            builder.RegisterInstance<LiquidView>(liquidView);
            builder.RegisterInstance<NameInputView>(nameInputView);
            builder.RegisterInstance<TimeView>(timeView);
            builder.RegisterInstance<TitleView>(titleView);
            builder.RegisterInstance<UserRecordView>(userRecordView);
            builder.RegisterInstance<VolumeView>(volumeView);
        }
    }
}