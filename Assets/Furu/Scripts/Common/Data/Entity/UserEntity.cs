namespace Furu.Common.Data.Entity
{
    public sealed class UserEntity
    {
        public string userName { get; private set; }
        public string userId { get; private set; }

        public void Set(UserEntity entity)
        {
            SetUserName(entity.userName);
            SetUserId(entity.userId);
        }

        public void SetUserName(string name)
        {
            userName = name;
        }

        public void SetUserId(string id)
        {
            userId = id;
        }

        public bool IsEmptyUserName()
        {
            return string.IsNullOrEmpty(userName);
        }
    }
}