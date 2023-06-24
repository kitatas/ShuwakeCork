using Furu.Common.Data.Entity;
using Furu.InGame.Data.Entity;
using Furu.InGame.Domain.Repository;

namespace Furu.InGame.Domain.UseCase
{
    public sealed class LiquidUseCase
    {
        private LiquidEntity _liquidEntity;
        private readonly UserEntity _userEntity;
        private readonly LiquidRepository _liquidRepository;

        public LiquidUseCase(UserEntity userEntity, LiquidRepository liquidRepository)
        {
            _userEntity = userEntity;
            _liquidRepository = liquidRepository;
        }

        public LiquidEntity Lot()
        {
            _liquidEntity = _liquidRepository.Lot(_userEntity.playEntity.playCount);
            return _liquidEntity;
        }
    }
}