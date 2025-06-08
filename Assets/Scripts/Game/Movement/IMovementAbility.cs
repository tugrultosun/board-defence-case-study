using UnityEngine;

namespace Game.Movement
{
    public interface IMovementAbility
    {
        public float Speed { get; set; }
        
        public void Movement(Transform transform);
    }
}
