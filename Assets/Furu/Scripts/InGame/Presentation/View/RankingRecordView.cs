using TMPro;
using UniEx;
using UnityEngine;
using UnityEngine.UI;

namespace Furu.InGame.Presentation.View
{
    public sealed class RankingRecordView : MonoBehaviour
    {
        [SerializeField] private Image background = default;
        [SerializeField] private TextMeshProUGUI rank = default;
        [SerializeField] private TextMeshProUGUI userName = default;
        [SerializeField] private TextMeshProUGUI score = default;

        public void SetData(Common.Data.Entity.RankingRecordEntity entity)
        {
            if (entity.isSelf)
            {
                background.SetColorA(1.0f);
            }

            rank.text = $"{entity.rank.ToString()}";
            userName.text = $"{entity.name}";
            score.text = $"{entity.GetScore().ToString("F2")}";
        }
    }
}