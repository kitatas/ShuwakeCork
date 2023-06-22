using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Boot.Domain.UseCase;
using Furu.Boot.Presentation.View;
using Furu.Common;

namespace Furu.Boot.Presentation.Controller
{
    public sealed class LoginState : BaseState
    {
        private readonly LoginUseCase _loginUseCase;
        private readonly RegisterView _registerView;

        public LoginState(LoginUseCase loginUseCase, RegisterView registerView)
        {
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
                var userName = await _registerView.DecisionNameAsync(UiConfig.POPUP_TIME, token);

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