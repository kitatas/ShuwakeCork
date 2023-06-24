using System;
using Furu.Common.Domain.Repository;
using UniRx;
using UnityEngine;

namespace Furu.Common.Domain.UseCase
{
    public sealed class SoundUseCase
    {
        private readonly SoundRepository _soundRepository;

        private readonly Subject<Data.DataStore.BgmData> _playBgm;
        private readonly Subject<Data.DataStore.SeData> _playSe;

        public SoundUseCase(SoundRepository soundRepository)
        {
            _soundRepository = soundRepository;

            _playBgm = new Subject<Data.DataStore.BgmData>();
            _playSe = new Subject<Data.DataStore.SeData>();
        }

        public IObservable<AudioClip> playBgm => _playBgm.Select(x => x.clip);
        public IObservable<AudioClip> playSe => _playSe.Select(x => x.clip);

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
    }
}