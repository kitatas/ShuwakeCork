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
    }
}