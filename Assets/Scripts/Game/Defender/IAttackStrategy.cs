using UnityEngine;

namespace Game.Defender
{
    public interface IAttackStrategy
    {
        public int Range { get; set; }
        public bool ShouldAttack(Transform defender, Transform enemy);
    }
}
