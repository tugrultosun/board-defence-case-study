using Game.Defender;
using Managers;
using UnityEngine;

namespace Game.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void Initialize(DefenderType defenderType)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetProjectileSprite(defenderType);
        }
    }
}
