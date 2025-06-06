using UnityEngine;

namespace Game.Defender
{
    public class AllDirectionAttackStrategy : IAttackStrategy
    {
        public int Range { get; set; }

        public AllDirectionAttackStrategy(int range)
        {
            Range = range;
        }

        public bool ShouldAttack(Transform defender, Transform enemy)
        {
            var distanceToEnemy = Vector2.Distance(defender.position, enemy.transform.position);
            var isInRange = distanceToEnemy <= Range;
            if (isInRange)
            {
                return true;
            }
            return false;
        }
    }
}
