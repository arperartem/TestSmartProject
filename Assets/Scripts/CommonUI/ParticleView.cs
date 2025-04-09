using UnityEngine;

namespace CommonUI
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleView : MonoBehaviour
    {
        [SerializeField] private bool disableAtStop;
        [SerializeField] private ParticleSystem ps;

        public bool IsAlive => ps.IsAlive();

        public void Play() => ps.Play();
        
        private void OnParticleSystemStopped()
        {
            if (disableAtStop)
                gameObject.SetActive(false);
        }
    }
}