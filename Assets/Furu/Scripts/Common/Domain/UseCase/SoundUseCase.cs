using System;
using Furu.Common.Data.Entity;
using Furu.Common.Domain.Repository;
using UniRx;
using UnityEngine;

namespace Furu.Common.Domain.UseCase
{
    public sealed class SoundUseCase
    {
        private readonly SaveRepository _saveRepository;
        private readonly SoundRepository _soundRepository;

        private readonly Subject<Data.DataStore.BgmData> _playBgm;
        private readonly Subject<SoundEntity> _playSe;
        private readonly ReactiveProperty<float> _bgmVolume;
        private readonly ReactiveProperty<float> _seVolume;

        public SoundUseCase(SaveRepository saveRepository, SoundRepository soundRepository)
        {
            _saveRepository = saveRepository;
            _soundRepository = soundRepository;

            var data = _saveRepository.Load();
            _playBgm = new Subject<Data.DataStore.BgmData>();
            _playSe = new Subject<SoundEntity>();
            _bgmVolume = new ReactiveProperty<float>(data.bgmVolume);
            _seVolume = new ReactiveProperty<float>(data.seVolume);
        }

        public IObservable<AudioClip> playBgm => _playBgm.Select(x => x.clip);
        public IObservable<SoundEntity> playSe => _playSe.Where(x => x.clip != null);
        public IObservable<float> bgmVolume => _bgmVolume;
        public IObservable<float> seVolume => _seVolume;
        public float bgmVolumeValue => _bgmVolume.Value;
        public float seVolumeValue => _seVolume.Value;

        public void PlayBgm(BgmType type)
        {
            var data = _soundRepository.Find(type);
            _playBgm?.OnNext(data);
        }

        public void PlaySe(SeType type)
        {
            PlaySe(type, 0.0f);
        }

        public void PlaySe(SeType type, float delay)
        {
            var data = _soundRepository.Find(type);
            var soundEntity = new SoundEntity(data.clip, delay);
            _playSe?.OnNext(soundEntity);
        }

        public void SetBgmVolume(float value)
        {
            _bgmVolume.Value = value;
            _saveRepository.SaveBgm(value);
        }

        public void SetSeVolume(float value)
        {
            _seVolume.Value = value;
            _saveRepository.SaveSe(value);
        }
    }
}