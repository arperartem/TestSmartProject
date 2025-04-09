using CommonUI;
using UnityEngine;

namespace Particles
{
    public class ParticleHolderView : MonoBehaviour, IParticlePlayer
    {
        [SerializeField] private ParticleView starsParticle;
        
        public ParticleView PlayStarsParticle()
        {
            starsParticle.gameObject.SetActive(true);
            starsParticle.Play();
            return starsParticle;
        }
    }
}