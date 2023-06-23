using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common.Domain.UseCase;
using Furu.Common.Presentation.View;
using UniRx;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Furu.Common.Presentation.Presenter
{
    public sealed class ScenePresenter : IInitializable, IDisposable
    {
        private readonly SceneUseCase _sceneUseCase;
        private readonly TransitionView _transitionView;
        private readonly CancellationTokenSource _tokenSource;

        public ScenePresenter(SceneUseCase sceneUseCase, TransitionView transitionView)
        {
            _sceneUseCase = sceneUseCase;
            _transitionView = transitionView;
            _tokenSource = new CancellationTokenSource();
        }

        public void Initialize()
        {
            _sceneUseCase.load
                .Subscribe(x =>
                {
                    // シーン遷移
                    switch (x.loadType)
                    {
                        case LoadType.Direct:
                            SceneManager.LoadScene(x.sceneName.ToString());
                            break;
                        case LoadType.Fade:
                            FadeLoadAsync(x.sceneName, _tokenSource.Token).Forget();
                            break;
                        default:
                            throw new CrashException(ExceptionConfig.UNMATCHED_LOAD_TYPE);
                    }
                })
                .AddTo(_tokenSource.Token);
        }

        private async UniTaskVoid FadeLoadAsync(SceneName sceneName, CancellationToken token)
        {
            await _transitionView.FadeInAsync(SceneConfig.FADE_TIME, token);
            await SceneManager.LoadSceneAsync(sceneName.ToString()).WithCancellation(token);
            await _transitionView.FadeOutAsync(SceneConfig.FADE_TIME, token);
        }

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}