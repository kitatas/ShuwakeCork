using Newtonsoft.Json;

namespace Furu.Common.Data.Entity
{
    public sealed class UserPlayEntity
    {
        public int playCount;
        public RecordEntity distance;

        public static UserPlayEntity Default()
        {
            return new UserPlayEntity
            {
                playCount = 0,
                distance = RecordEntity.Default(),
            };
        }

        public UserPlayEntity UpdateByPlay(float distanceScore)
        {
            return new UserPlayEntity
            {
                playCount = playCount + 1,
                distance = distance.Update(distanceScore),
            };
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public sealed class RecordEntity
    {
        public float current;
        public float high;

        public static RecordEntity Default()
        {
            return new RecordEntity
            {
                current = 0.0f,
                high = 0.0f,
            };
        }

        public RecordEntity Update(float score)
        {
            return new RecordEntity
            {
                current = score,
                high = score > high ? score : high,
            };
        }

        public int GetCurrentForRanking()
        {
            return (int)(current * PlayFabConfig.SCORE_RATE);
        }
    }
}