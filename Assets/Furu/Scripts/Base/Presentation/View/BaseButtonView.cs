using System;
using DG.Tweening;
using Furu.Common;
using UniEx;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Furu.Base.Presentation.View
{
    public abstract class BaseButtonView : MonoBehaviour
    {
        [SerializeField] private Button button = default;
        public Action pushed;

        private readonly float _animationTime = 0.1f;
        
        protected IObservable<Unit> push => button.OnClickAsObservable();

        public virtual void Init(Action<SeType> playSe)
        {
            var rectTransform = button.transform.ToRectTransform();
            var scale = rectTransform.localScale;

            pushed += () =>
            {
                // 押下時のアニメーション
                DOTween.Sequence()
                    .Append(rectTransform
                        .DOScale(scale * 0.8f, _animationTime))
                    .Append(rectTransform
                        .DOScale(scale, _animationTime))
                    .SetLink(gameObject);

                // 押下時の効果音
                playSe?.Invoke(SeType.Decision);
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