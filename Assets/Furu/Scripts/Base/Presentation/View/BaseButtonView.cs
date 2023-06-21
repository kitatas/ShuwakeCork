using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Furu.Base.Presentation.View
{
    public abstract class BaseButtonView : MonoBehaviour
    {
        [SerializeField] private Button button = default;
        public Action pushed;

        protected IObservable<Unit> push => button.OnClickAsObservable();

        public virtual void Init()
        {
            pushed += () =>
            {
                // TODO: 押下時のアニメーション
                // TODO: 押下時の効果音
            };

            push.Subscribe(_ => pushed?.Invoke())
                .AddTo(this);
        }

        protected Image image => button.image;

        protected void Activate(bool value)
        {
            button.enabled = value;
        }
    }
}