using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Boot.Domain.UseCase;
using Furu.Boot.Presentation.Controller;
using UniRx;
using VContainer.Unity;

namespace Furu.Boot.Presentation.Presenter
{
    public sealed class StatePresenter : IInitializable, IDisposable
    {
        private readonly StateUseCase _stateUseCase;
        private readonly StateController _stateController;
        private readonly CancellationTokenSource _tokenSource;

        public StatePresenter(StateUseCase stateUseCase, StateController stateController)
        {
            _stateUseCase = stateUseCase;
            _stateController = stateController;
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _stateController.InitAsync(_tokenSource.Token).Forget();

            _stateUseCase.bootState
                .Subscribe(x => ExecAsync(x, _tokenSource.Token).Forget())
                .AddTo(_tokenSource.Token);
        }

        private async UniTask ExecAsync(BootState state, CancellationToken token)
        {
            try
            {
                var nextState = await _stateController.TickAsync(state, token);
                _stateUseCase.Set(nextState);
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogError($"{state}: {e}");
                throw;
            }
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}