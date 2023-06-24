using System;
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
        private readonly Subject<Data.DataStore.SeData> _playSe;
        private readonly ReactiveProperty<float> _bgmVolume;
        private readonly ReactiveProperty<float> _seVolume;

        public SoundUseCase(SaveRepository saveRepository, SoundRepository soundRepository)
        {
            _saveRepository = saveRepository;
            _soundRepository = soundRepository;

            var data = _saveRepository.Load();
            _playBgm = new Subject<Data.DataStore.BgmData>();
            _playSe = new Subject<Data.DataStore.SeData>();
            _bgmVolume = new ReactiveProperty<float>(data.bgmVolume);
            _seVolume = new ReactiveProperty<float>(data.seVolume);
        }

        public IObservable<AudioClip> playBgm => _playBgm.Select(x => x.clip);
        public IObservable<AudioClip> playSe => _playSe.Select(x => x.clip);
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
            var data = _soundRepository.Find(type);
            _playSe?.OnNext(data);
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