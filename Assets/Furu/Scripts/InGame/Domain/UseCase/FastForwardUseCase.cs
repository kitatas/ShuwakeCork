using System;
using Furu.Common.Domain.Repository;
using UniRx;
using UnityEngine;

namespace Furu.InGame.Domain.UseCase
{
    public sealed class FastForwardUseCase
    {
        private readonly SaveRepository _saveRepository;

        private readonly ReactiveProperty<bool> _active;

        public FastForwardUseCase(SaveRepository saveRepository)
        {
            _saveRepository = saveRepository;

            var data = _saveRepository.Load();
            _active = new ReactiveProperty<bool>(data.isFast);
        }

        public IObservable<bool> active => _active;

        public void Switch()
        {
            _active.Value = !_active.Value;
        }

        public void Set(bool value)
        {
            _saveRepository.SaveFast(value);
            Time.timeScale = value ? GameConfig.FAST_SPEED : GameConfig.DEFAULT_SPEED;

            Debug.Log($"scale: {Time.timeScale}");
        }

        public void Reset()
        {
            Time.timeScale = GameConfig.DEFAULT_SPEED;
        }
    }
}