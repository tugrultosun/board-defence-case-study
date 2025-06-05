using System.Collections;
using DG.Tweening;
using Game.Board;
using Game.Enemies;
using Game.Projectiles;
using Lean.Pool;
using Managers;
using Settings;
using UnityEngine;

namespace Game.Defender
{
    public class Defender : MonoBehaviour, IDefender
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        [SerializeField] private GameObject projectilePrefab;
        public DefenderType DefenderType { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public int AttackRate { get; set; }
        public bool CanAttack { get; set; }
        public DefenderAttackDirection AttackDirection { get; set; }

        public void Initialize(DefenderDataModel defenderDataModel)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetDefenderSprite(defenderDataModel.defenderType);
            DefenderType = defenderDataModel.defenderType;
            Damage = defenderDataModel.damage;
            Range = defenderDataModel.range;
            AttackRate = defenderDataModel.attackRate;
            AttackDirection = defenderDataModel.direction;
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
                    var distanceToEnemy = Vector2.Distance(transform.position,BoardManager.Instance.EnemyController.Enemies[i].transform.position);
                    if(distanceToEnemy <= Range)
                    {
                        if(AttackDirection == DefenderAttackDirection.ForwardDirection && Mathf.Abs(transform.position.x - BoardManager.Instance.EnemyController.Enemies[i].transform.position.x) < GameSettingsManager.Instance.boardSettings.defenderAttackThreshold)
                        {
                            Debug.Log($"defender :{DefenderType} is attacking direction{AttackDirection} with dmg {Damage}");
                            Debug.Log($"defender x:{transform.position.x} enemy x: {BoardManager.Instance.EnemyController.Enemies[i].transform.position.x}");
                            Shoot(BoardManager.Instance.EnemyController.Enemies[i]);
                        }
                        else if(AttackDirection == DefenderAttackDirection.AllDirection)
                        {
                            Debug.Log($"defender :{DefenderType} is attacking direction{AttackDirection} with dmg {Damage}");
                            Shoot(BoardManager.Instance.EnemyController.Enemies[i]);
                        }
                    }
                }
            }
        }

        private void Shoot(Enemy enemy)
        {
            var projectileGo = LeanPool.Spawn(projectilePrefab);
            var projectileComponent = projectileGo.GetComponent<Projectile>();
            if (projectileComponent != null)
            {
                projectileComponent.Initialize(DefenderType,Damage,enemy);
                projectileGo.transform.position = transform.position;
            }
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