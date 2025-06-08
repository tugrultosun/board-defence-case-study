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
            float sqrDistance = (defender.position - enemy.transform.position).sqrMagnitude;
            var isInRange = sqrDistance <= Range * Range;
            return isInRange;
        }
    }
}
