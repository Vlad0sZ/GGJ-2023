using UnityEngine;

namespace Main
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Character : MonoBehaviour
    {
        public virtual void Move(Vector2 direction)
        {
        
        }

        private void Interact()
        {
        
        }
    }
}