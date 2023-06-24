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

        public SoundUseCase(SoundRepository soundRepository)
        {
            _soundRepository = soundRepository;

            _playBgm = new Subject<Data.DataStore.BgmData>();
        }

        public IObservable<AudioClip> playBgm => _playBgm.Select(x => x.clip);

        public void PlayBgm(BgmType type)
        {
            var data = _soundRepository.Find(type);
            _playBgm?.OnNext(data);
        }
    }
}