namespace Furu.Common.Data.Entity
{
    public sealed class UserEntity
    {
        public string userName { get; private set; }
        public string userId { get; private set; }
        public UserPlayEntity playEntity { get; private set; }

        public void Set(UserEntity entity)
        {
            SetUserName(entity.userName);
            SetUserId(entity.userId);
            SetPlay(entity.playEntity);
        }

        public void SetUserName(string name)
        {
            userName = name;
        }

        public void SetUserId(string id)
        {
            userId = id;
        }

        public void SetPlay(UserPlayEntity play)
        {
            playEntity = play;
        }

        public bool IsEmptyUserName()
        {
            return string.IsNullOrEmpty(userName);
        }
    }
}