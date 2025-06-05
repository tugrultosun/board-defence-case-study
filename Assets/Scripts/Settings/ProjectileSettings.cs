using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "ProjectileSettings", menuName = "Scriptable Objects/Projectile Settings", order = 1)]
    public class ProjectileSettings : ScriptableObject
    {
        public int projectileSpeed;
        public float projectileTargetReachedOffset;
    }
}
