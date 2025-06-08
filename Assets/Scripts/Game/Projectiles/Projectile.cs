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
        public void Initialize(Transform spawnPos,DefenderType defenderType,int damageDeal,Enemy enemy)
        {
            transform.position = spawnPos.position;
            spriteRenderer.sprite = SpriteManager.Instance.GetProjectileSprite(defenderType);
            damage = damageDeal;
            target = enemy;
        }
        
        public void Update()
        {
            if (target != null)
            {
                Vector3 toTarget = target.transform.position - transform.position;
                float distance = toTarget.magnitude;
                if (distance <= GameSettingsManager.Instance.projectileSettings.projectileTargetReachedOffset)
                {
                    target.ApplyDamage(damage);
                    LeanPool.Despawn(gameObject);
                    return;
                }
                float moveStep = GameSettingsManager.Instance.projectileSettings.projectileSpeed * Time.deltaTime;
                Vector3 move = toTarget.normalized * Mathf.Min(distance, moveStep);
                transform.position += move;
                float angle = Mathf.Atan2(move.y, move.x) * Mathf.Rad2Deg - 90f;
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }
}
