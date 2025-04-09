using CommonUI;
using UnityEngine;

namespace Particles
{
    public class ParticleHolderView : MonoBehaviour, IParticlePlayer
    {
        [SerializeField] private ParticleView selectBoosterParticle;
        [SerializeField] private ParticleView sideCellParticle;
        
        public ParticleView PlayParticle(ParticleType type, Vector3 position)
        {
            ParticleView particle = null;
            
            switch (type)
            {
                case ParticleType.SelectBooster:
                    particle = selectBoosterParticle;
                    break;
                case ParticleType.SideCell:
                    particle = sideCellParticle;
                    break;
            }
            
            return InstantPlay(particle, position);
        }

        private ParticleView InstantPlay(ParticleView particle, Vector3 position)
        {
            particle.gameObject.SetActive(true);
            particle.transform.position = new Vector3(position.x, position.y, 0f);
            particle.Play();
            return particle;
        }
    }
}