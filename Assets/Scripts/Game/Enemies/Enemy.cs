using Game.Board;
using Lean.Pool;
using Managers;
using UnityEngine;

namespace Game.Enemies
{
    public class Enemy : MonoBehaviour , IEnemy
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        public EnemyType EnemyType { get; set; }
        public int Health { get; set; }
        public float Speed { get; set; }

        public void Initialize(EnemyDataModel enemyDataModel)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetEnemySprite(enemyDataModel.enemyType);
            EnemyType = enemyDataModel.enemyType;
            Health = enemyDataModel.health;
            Speed = enemyDataModel.speed;
        }

        public void ApplyDamage(int damage)
        {
            Health -= damage;
            Debug.Log("defender attacked, enemy hp is now:" + Health);
            if (Health <= 0)
            {
                BoardManager.Instance.RemoveEnemy(this);
                LeanPool.Despawn(gameObject);
            }
        }
    }
}
