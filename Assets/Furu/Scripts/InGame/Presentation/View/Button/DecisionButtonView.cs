using System;
using Furu.Base.Presentation.View;
using UniRx;

namespace Furu.InGame.Presentation.View
{
    public sealed class DecisionButtonView : BaseButtonView
    {
        public IObservable<Unit> Decision()
        {
            return push;
        }
    }
}