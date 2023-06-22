using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.InGame.Domain.UseCase;
using Furu.InGame.Presentation.View;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class FinishState : BaseState
    {
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly CorkView _corkView;
        private readonly UserRecordView _userRecordView;

        public FinishState(UserRecordUseCase userRecordUseCase, CorkView corkView, UserRecordView userRecordView)
        {
            _userRecordUseCase = userRecordUseCase;
            _corkView = corkView;
            _userRecordView = userRecordView;
        }

        public override GameState state => GameState.Finish;

        public override async UniTask InitAsync(CancellationToken token)
        {
            await UniTask.Yield(token);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // ユーザーの記録更新 + ランキング送信
            var distance = _corkView.flyingDistance;
            await _userRecordUseCase.SendScoreAsync(distance, token);

            var (distanceRecord, _) = _userRecordUseCase.GetUserScore();
            _userRecordView.SetDistanceScore(distanceRecord.current, distanceRecord.high);

            // ランキング反映待ち
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            return GameState.Result;
        }
    }
}