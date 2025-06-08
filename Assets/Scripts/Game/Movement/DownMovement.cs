using UnityEngine;

namespace Game.Movement
{
    public class DownMovement : IMovementAbility
    {
        public float Speed { get; set; }

        public DownMovement(float speed)
        {
            Speed = speed;
        }

        public void Movement(Transform transform)
        {
            transform.Translate(Vector3.down * (Speed * Time.deltaTime));
        }
    }
}
