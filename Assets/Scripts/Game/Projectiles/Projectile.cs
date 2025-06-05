using System;
using Game.Defender;
using Game.Enemies;
using Lean.Pool;
using Managers;
using Settings;
using UnityEngine;

namespace Game.Projectiles
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        private Enemy target;
        
        private int damage;
        public void Initialize(DefenderType defenderType,int damageDeal,Enemy enemy)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetProjectileSprite(defenderType);
            damage = damageDeal;
            target = enemy;
        }
        
        public void Update()
        {
            if (target != null)
            {
                Vector3 dir = (target.transform.position - transform.position).normalized;
                transform.position += dir * GameSettingsManager.Instance.projectileSettings.projectileSpeed * Time.deltaTime;
                var distance = Vector2.Distance(transform.position, target.transform.position);
                if (distance <= GameSettingsManager.Instance.projectileSettings.projectileTargetReachedOffset)
                {
                    target.ApplyDamage(damage);
                    LeanPool.Despawn(gameObject);
                    return;
                }
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
