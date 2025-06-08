using UnityEngine;

namespace Game.Helpers
{
    public static class DirectionHelper
    {
        public static bool IsInForwardDirection(Vector2 defenderPos, Vector2 enemyPos, float threshold)
        {
            return Mathf.Abs(defenderPos.x - enemyPos.x) < threshold;
        }
    }
}
