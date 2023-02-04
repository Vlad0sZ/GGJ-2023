using System;
using UnityEngine;

namespace Main
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Swinging : MonoBehaviour
    {
        [SerializeField] private HingeJoint2D hingeJoint2D;
        [SerializeField] private float detachImpulseForce;
        
        private Rigidbody2D _rigidbody;
        private IRope _lastRope;
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Attach(IRope rope)
        {
            hingeJoint2D.enabled = true;
            hingeJoint2D.connectedBody = rope.Rigidbody2D;
        }

        public void Detach()
        {
            hingeJoint2D.connectedBody = null;
            hingeJoint2D.enabled = false;
            _rigidbody.AddForce(detachImpulseForce * Vector2.up, ForceMode2D.Impulse);
        }

        public void Climb(Vector2 direction)
        {
            
        }
    }
}