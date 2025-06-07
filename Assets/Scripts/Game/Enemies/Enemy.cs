using Events;
using Game.Board;
using Lean.Pool;
using Managers;
using Settings;
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
        private bool IsDead => Health <= 0;

        private bool triggeredBreachEvent;

        public void Initialize(EnemyDataModel enemyDataModel)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetEnemySprite(enemyDataModel.enemyType);
            EnemyType = enemyDataModel.enemyType;
            Health = enemyDataModel.health;
            Speed = enemyDataModel.speed;
            CanMove = true;// can be changed based on events or other things
            hpText.SetText(Health.ToString());
            triggeredBreachEvent = false;
        }

        public void ApplyDamage(int damage)
        {
            if (IsDead)
            {
                //since two defence item can attack same enemy
                Debug.Log("enemy is already dead");
                return;
            }
            Health -= damage;
            hpText.SetText(Health.ToString());
            Debug.Log("defender attacked, enemy hp is now:" + Health);
            var hitParticleSystem = ParticleManager.Instance.GetHitParticles();
            hitParticleSystem.transform.position = transform.position;
            if (IsDead)
            {
                BoardManager.Instance.RemoveEnemy(this);
                var explodeParticleSystem = ParticleManager.Instance.GetExplodeParticles();
                explodeParticleSystem.transform.position = transform.position;
                LeanPool.Despawn(gameObject);
            }
        }

        public void Move()
        {
            if (CanMove)
            {
                transform.Translate(Vector3.down * (Speed * Time.deltaTime));
                if (triggeredBreachEvent == false && transform.position.y < GameSettingsManager.Instance.boardSettings.enemyBreachYPos )
                {
                    triggeredBreachEvent = true;
                    EventManager.Instance.TriggerEvent<GameFinishedEvent>(new GameFinishedEvent{IsWin = false});
                }
            }
        }

        private void Update()
        {
            Move();
        }
    }
}
