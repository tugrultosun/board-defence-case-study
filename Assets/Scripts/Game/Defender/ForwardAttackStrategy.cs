using Settings;
using UnityEngine;

namespace Game.Defender
{
    public class ForwardAttackStrategy : IAttackStrategy
    {

        public int Range { get; set; }
        
        public ForwardAttackStrategy(int range)
        {
            Range = range;
        }

        

        public bool ShouldAttack(Transform defender, Transform enemy)
        {
            var distanceToEnemy = Vector2.Distance(defender.position, enemy.transform.position);
            var isInRange = distanceToEnemy <= Range;
            var isInForwardDirection =  Mathf.Abs(defender.position.x - enemy.position.x) < GameSettingsManager.Instance.boardSettings.defenderAttackThreshold;
            if (isInRange && isInForwardDirection)
            {
                return true;
            }
            return false;
        }
    }
}
