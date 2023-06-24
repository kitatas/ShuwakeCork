using UniEx;
using UnityEngine;

namespace Furu.InGame.Presentation.View
{
    public sealed class SplashView : MonoBehaviour
    {
        // [SerializeField] private ParticleSystem particle = default;
        [SerializeField] private ParticleSystem[] particles = default;
        private int _index;

        public void Init()
        {
            // particle.Stop();
            particles.Each(x => x.Stop());
        }

        public void SetUp(Data.Entity.LiquidEntity entity)
        {
            // TODO: 要修正
            // entity の color から動的に gradient を変更する
            // var colorOverLifetimeModule = particle.colorOverLifetime;

            _index = entity.id - 1;
        }

        public void Play()
        {
            // particle.Play();
            particles[_index].Play();
        }
    }
}