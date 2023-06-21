using System.Collections.Generic;
using Furu.Common.Data.Entity;
using PlayFab.ClientModels;

namespace Furu.Common.Data.DataStore
{
    public sealed class UserData
    {
        private readonly Dictionary<string, UserDataRecord> _records;
        public readonly UserEntity user;

        public UserData(string name, string id, Dictionary<string, UserDataRecord> records)
        {
            _records = records;

            user = new UserEntity();
            user.SetUserName(name);
            user.SetUserId(id);
        }
    }
}