using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common.Domain.UseCase;
using UniRx;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Furu.Common.Presentation.Presenter
{
    public sealed class ScenePresenter : IInitializable, IDisposable
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly CancellationTokenSource _tokenSource;

        public ScenePresenter(SceneUseCase sceneUseCase)
        {
            _sceneUseCase = sceneUseCase;
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _sceneUseCase.load
                .Subscribe(x =>
                {
                    // TODO: Fadeを考える
                    SceneManager.LoadScene(x.ToString());
                })
                .AddTo(_tokenSource.Token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}