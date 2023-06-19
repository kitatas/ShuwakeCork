using Furu.Base.Domain.UseCase;
using UniEx;
using UnityEngine;

namespace Furu.InGame.Domain.UseCase
{
    public sealed class TimeUseCase : BaseModelUseCase<float>
    {
        public TimeUseCase()
        {
            Set(GameConfig.SHAKE_TIME);
        }

        public void Decrease(float deltaTime)
        {
            Set(Mathf.Max(property.Value - deltaTime, 0.0f));
        }

        public bool IsTimeUp()
        {
            return property.Value.IsZero();
        }
    }
}