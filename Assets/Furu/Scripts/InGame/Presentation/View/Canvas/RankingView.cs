using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class RankingView : BaseCanvasGroupView
    {
        [SerializeField] private RectTransform viewport = default;
        [SerializeField] private RankingRecordView recordView = default;

        [SerializeField] private ReloadButtonView reloadButtonView = default;

        public void SetUp(List<Common.Data.Entity.DistanceRecordEntity> recordEntities)
        {
            foreach (var entity in recordEntities)
            {
                var record = Instantiate(recordView, viewport);
                record.SetData(entity);
            }
        }

        public async UniTask ReloadAsync(float animationTime, CancellationToken token)
        {
            await ShowAsync(animationTime, token);

            await reloadButtonView.ShowAsync(animationTime, token);

            await reloadButtonView.PushAsync(token);
        }
    }
}