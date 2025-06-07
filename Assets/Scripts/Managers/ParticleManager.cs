using Lean.Pool;
using UnityEngine;

namespace Managers
{
    public class ParticleManager : MonoSingleton<ParticleManager>
    {
        public ParticleSystem explodeParticles;
        
        public ParticleSystem hitParticles;


        public ParticleSystem GetExplodeParticles()
        {
            return LeanPool.Spawn(explodeParticles, transform.position, Quaternion.identity);
        }

        public ParticleSystem GetHitParticles()
        {
            return LeanPool.Spawn(hitParticles, transform.position, Quaternion.identity);
        }
    }
}
