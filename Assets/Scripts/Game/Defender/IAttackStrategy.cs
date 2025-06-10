using UnityEngine;

namespace Game.Defender
{
    public interface IAttackStrategy
    {
        public int Range { get; }
        public bool ShouldAttack(Transform defender, Transform enemy);
    }
}
