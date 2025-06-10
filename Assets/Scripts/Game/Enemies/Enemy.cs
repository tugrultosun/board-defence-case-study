using Events;
using Game.Board;
using Game.Movement;
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
        public EnemyType EnemyType { get; private set; }
        public int Health { get; private set; }
        public float Speed { get; private set; }
        public bool CanMove { get; private set; }
        public IMovementAbility MovementAbility { get; private set; }
        public bool IsDead => Health <= 0;

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
            MovementAbility = new DownMovement(Speed);
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
            ParticleManager.Instance.GetHitParticles(transform);
            if (IsDead)
            {
                BoardManager.Instance.RemoveDeadEnemy(this);
                ParticleManager.Instance.GetExplodeParticles(transform);
                LeanPool.Despawn(gameObject);
            }
        }
        
        private void Update()
        {
            Move();
            CheckIfCrossedBottomOfBoard();
        }
        
        public void Move()
        {
            if (CanMove)
            {
                MovementAbility.Movement(transform);
            }
        }
        
        private void CheckIfCrossedBottomOfBoard()
        {
            if (triggeredBreachEvent == false && transform.position.y < GameSettingsManager.Instance.boardSettings.enemyBreachYPos )
            {
                triggeredBreachEvent = true;
                EventManager.Instance.TriggerEvent<GameFinishedEvent>(new GameFinishedEvent{IsWin = false});
            }
        }
    }
}
