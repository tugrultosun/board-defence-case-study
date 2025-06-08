using Game.Helpers;
using Settings;
using UnityEngine;

namespace Game.Defender
{
    public class ForwardAttackStrategy : IAttackStrategy
    {
        public int Range { get; set; }

        private readonly float thresholdForCheckingIfItsInForwardDirection;
        
        public ForwardAttackStrategy(int range)
        {
            Range = range;
            thresholdForCheckingIfItsInForwardDirection = GameSettingsManager.Instance.boardSettings.defenderAttackThreshold;
        }
        
        public bool ShouldAttack(Transform defender, Transform enemy)
        {
            var sqrDistanceToEnemy = (defender.position - enemy.transform.position).sqrMagnitude;
            var isInRange = sqrDistanceToEnemy <= Range * Range;
            var isInForwardDirection = DirectionHelper.IsInForwardDirection(defender.position,enemy.position,thresholdForCheckingIfItsInForwardDirection);
            return isInRange && isInForwardDirection;
        }
    }
}
