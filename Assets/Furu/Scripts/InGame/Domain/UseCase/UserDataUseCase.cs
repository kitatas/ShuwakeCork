using System.Threading;
using Cysharp.Threading.Tasks;
using Furu.Common.Data.Entity;
using Furu.Common.Domain.Repository;

namespace Furu.InGame.Domain.UseCase
{
    public sealed class UserDataUseCase
    {
        private readonly UserEntity _userEntity;
        private readonly PlayFabRepository _playFabRepository;
        private readonly SaveRepository _saveRepository;

        public UserDataUseCase(UserEntity userEntity, PlayFabRepository playFabRepository,
            SaveRepository saveRepository)
        {
            _userEntity = userEntity;
            _playFabRepository = playFabRepository;
            _saveRepository = saveRepository;
        }

        public string GetUserName()
        {
            return _userEntity.userName;
        }

        public async UniTask<bool> UpdateUserNameAsync(string name, CancellationToken token)
        {
            var isSuccess = await _playFabRepository.UpdateUserNameAsync(name, token);
            if (isSuccess)
            {
                _userEntity.SetUserName(name);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Delete()
        {
            _saveRepository.Delete();
        }
    }
}