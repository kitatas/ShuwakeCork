using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Boot.Domain.UseCase;
using Furu.Boot.Presentation.View;
using Furu.Common;
using Furu.Common.Domain.UseCase;

namespace Furu.Boot.Presentation.Controller
{
    public sealed class LoginState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly RegisterView _registerView;

        public LoginState(LoadingUseCase loadingUseCase, LoginUseCase loginUseCase, RegisterView registerView)
        {
            _loadingUseCase = loadingUseCase;
            _loginUseCase = loginUseCase;
            _registerView = registerView;
        }

        public override BootState state => BootState.Login;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _registerView.HideAsync(0.0f, token).Forget();

            await UniTask.Yield(token);
        }

        public override async UniTask<BootState> TickAsync(CancellationToken token)
        {
            _loadingUseCase.Set(true);

            var isLoginSuccess = await _loginUseCase.LoginAsync(token);
            if (isLoginSuccess == false)
            {
                await RegisterAsync(token);
            }

            return BootState.Check;
        }

        private async UniTask RegisterAsync(CancellationToken token)
        {
            while (true)
            {
                _loadingUseCase.Set(false);

                var userName = await _registerView.DecisionNameAsync(UiConfig.POPUP_TIME, token);

                _loadingUseCase.Set(true);

                // 名前登録
                var isSuccess = await _loginUseCase.RegisterAsync(userName, token);
                if (isSuccess)
                {
                    break;
                }
            }
        }
    }
}