using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common.Domain.UseCase;
using Furu.InGame.Domain.UseCase;
using Furu.InGame.Presentation.View;

namespace Furu.InGame.Presentation.Controller
{
    public sealed class FinishState : BaseState
    {
        private readonly LoadingUseCase _loadingUseCase;
        private readonly LiquidUseCase _liquidUseCase;
        private readonly UserRecordUseCase _userRecordUseCase;
        private readonly CorkView _corkView;
        private readonly UserRecordView _userRecordView;

        public FinishState(LoadingUseCase loadingUseCase, LiquidUseCase liquidUseCase,
            UserRecordUseCase userRecordUseCase, CorkView corkView, UserRecordView userRecordView)
        {
            _loadingUseCase = loadingUseCase;
            _liquidUseCase = liquidUseCase;
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
            _loadingUseCase.Set(true);

            // ユーザーの記録更新 + ランキング送信
            var distance = _corkView.flyingDistance;
            var height = _corkView.height;
            await _userRecordUseCase.SendScoreAsync(distance, height, token);

            var (distanceRecord, heightRecord) = _userRecordUseCase.GetUserScore();
            var liquidName = _liquidUseCase.GetLiquidName();
            _userRecordView.SetDistanceScore(distanceRecord.current, distanceRecord.high, liquidName);
            _userRecordView.SetHeightScore(heightRecord.current, heightRecord.high, liquidName);

            // ランキング反映待ち
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            return GameState.Result;
        }
    }
}