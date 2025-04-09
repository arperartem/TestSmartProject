using CommonUI;
using UnityEngine;

namespace Particles
{
    public interface IParticlePlayer
    {
        ParticleView PlayParticle(ParticleType type, Vector3 position);
    }
}