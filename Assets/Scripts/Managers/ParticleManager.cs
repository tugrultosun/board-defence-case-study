using Lean.Pool;
using UnityEngine;
using Utils;

namespace Managers
{
    public class ParticleManager : MonoSingleton<ParticleManager>
    {
        public ParticleSystem explodeParticles;
        
        public ParticleSystem hitParticles;


        public ParticleSystem GetExplodeParticles(Transform spawnPoint)
        {
            return LeanPool.Spawn(explodeParticles, spawnPoint.position, Quaternion.identity);
        }

        public ParticleSystem GetHitParticles(Transform spawnPoint)
        {
            return LeanPool.Spawn(hitParticles, spawnPoint.position, Quaternion.identity);
        }
    }
}
