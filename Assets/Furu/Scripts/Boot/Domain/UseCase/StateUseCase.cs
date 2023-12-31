using System;
using Furu.Base.Domain.UseCase;
using UniRx;

namespace Furu.Boot.Domain.UseCase
{
    public sealed class StateUseCase : BaseModelUseCase<BootState>
    {
        public StateUseCase()
        {
            Set(BootState.Load);
        }

        public IObservable<BootState> bootState => property.Where(x => x != BootState.None);
    }
}