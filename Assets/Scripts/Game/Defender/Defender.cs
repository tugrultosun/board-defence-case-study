using System.Collections;
using Game.Board;
using Game.Enemies;
using Game.Projectiles;
using Lean.Pool;
using Managers;
using UnityEngine;

namespace Game.Defender
{
    public class Defender : MonoBehaviour, IDefender
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        [SerializeField] private Projectile projectilePrefab;
        public DefenderType DefenderType { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public int AttackRate { get; set; }
        public bool CanAttack { get; set; }
        public DefenderAttackDirection AttackDirection { get; set; }
        
        public IAttackStrategy Strategy { get; set; }

        public void Initialize(DefenderDataModel defenderDataModel)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetDefenderSprite(defenderDataModel.defenderType);
            DefenderType = defenderDataModel.defenderType;
            Damage = defenderDataModel.damage;
            Range = defenderDataModel.range;
            AttackRate = defenderDataModel.attackRate;
            AttackDirection = defenderDataModel.direction;
            Strategy = GetStrategy(AttackDirection);
        }

        private IAttackStrategy GetStrategy(DefenderAttackDirection defenderAttackDirection)
        {
            switch (defenderAttackDirection)
            {
                case DefenderAttackDirection.ForwardDirection:
                    return new ForwardAttackStrategy(Range);
                case DefenderAttackDirection.AllDirection:
                    return new AllDirectionAttackStrategy(Range);
                default:
                    Debug.LogWarning($"Defender strategy{defenderAttackDirection} not implemented");
                    return new ForwardAttackStrategy(Range);
            }
        }

        public void Activate()
        {
            CanAttack = true;
        }

        public void Deactivate()
        {
            CanAttack = false;
        }

        public void Update()
        {
            if (CanAttack)
            {
                for(int i = 0 ; i < BoardManager.Instance.EnemyController.Enemies.Count ; i++)
                {
                    Enemy enemy = BoardManager.Instance.EnemyController.Enemies[i];
                    if(Strategy.ShouldAttack(transform,enemy.transform))
                    {
                        Debug.Log($"defender :{DefenderType} is attacking direction{AttackDirection} with dmg {Damage}");
                        Shoot(enemy);
                        break; //so dont attack anymore enemies
                    }
                }
            }
        }

        private void Shoot(Enemy enemy)
        {
            var projectile = LeanPool.Spawn<Projectile>(projectilePrefab);
            projectile.Initialize(DefenderType, Damage, enemy);
            projectile.transform.position = transform.position;
            StartCoroutine(ApplyCooldown());
        }

        private IEnumerator ApplyCooldown()
        {
            CanAttack = false;
            yield return new WaitForSeconds(AttackRate);
            CanAttack = true;
        }
    }
}