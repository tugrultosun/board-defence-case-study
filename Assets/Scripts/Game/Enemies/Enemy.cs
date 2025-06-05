using System;
using Game.Board;
using Lean.Pool;
using Managers;
using TMPro;
using UnityEngine;

namespace Game.Enemies
{
    public class Enemy : MonoBehaviour , IEnemy
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        [SerializeField] private TextMeshPro hpText; 
        public EnemyType EnemyType { get; set; }
        public int Health { get; set; }
        public float Speed { get; set; }
        public bool CanMove { get; set; }

        public void Initialize(EnemyDataModel enemyDataModel)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetEnemySprite(enemyDataModel.enemyType);
            EnemyType = enemyDataModel.enemyType;
            Health = enemyDataModel.health;
            Speed = enemyDataModel.speed;
            CanMove = true;// can be changed based on events or other things
            hpText.SetText(Health.ToString());
        }

        public void ApplyDamage(int damage)
        {
            Health -= damage;
            hpText.SetText(Health.ToString());
            Debug.Log("defender attacked, enemy hp is now:" + Health);
            if (Health <= 0)
            {
                BoardManager.Instance.RemoveEnemy(this);
                LeanPool.Despawn(gameObject);
            }
        }

        public void Move()
        {
            if (CanMove)
            {
                transform.Translate(Vector3.down * Speed * Time.deltaTime);
            }
        }

        private void Update()
        {
            Move();
        }
    }
}
