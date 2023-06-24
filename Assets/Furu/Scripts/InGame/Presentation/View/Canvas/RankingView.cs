using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Base.Presentation.View;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class RankingView : BaseCanvasGroupView
    {
        [SerializeField] private RectTransform distanceViewport = default;
        [SerializeField] private RectTransform heightViewport = default;
        [SerializeField] private RankingRecordView recordView = default;

        [SerializeField] private ReloadButtonView reloadButtonView = default;

        public void SetUp(List<Common.Data.Entity.DistanceRecordEntity> distanceRecords,
            List<Common.Data.Entity.HeightRecordEntity> heightRecords)
        {
            foreach (var entity in distanceRecords)
            {
                var record = Instantiate(recordView, distanceViewport);
                record.SetData(entity);
            }

            foreach (var entity in heightRecords)
            {
                var record = Instantiate(recordView, heightViewport);
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