using System;
using UnityEngine;

namespace Main
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Swinging : MonoBehaviour
    {
        [SerializeField] private HingeJoint2D hingeJoint2D;
        [SerializeField] private float detachImpulseForce;
        [SerializeField] private float swingPower;
        [SerializeField] private float climbSpeed;
        
        private Rigidbody2D _rigidbody;
        private Vector2 _input;
        private IRope _attachedRope;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void Attach(IRope rope)
        {
            hingeJoint2D.enabled = true;
            hingeJoint2D.connectedBody = rope.Rigidbody2D;
            _attachedRope = rope;
        }

        public void Detach()
        {
            hingeJoint2D.connectedBody = null;
            hingeJoint2D.enabled = false;
            _rigidbody.AddForce(detachImpulseForce * Vector2.up + _input, ForceMode2D.Impulse);
            _attachedRope = null;
        }

        public void StopClimb()
        {
            _input = Vector2.zero;
        }

        public void Climb(Vector2 direction)
        {
            _input = direction;
            
            if (_input.x != 0)
            {
                _rigidbody.AddForce(Vector2.right * (_input.x * swingPower), ForceMode2D.Impulse);
            }
        }

        private void Update()
        {
            if(_attachedRope == null || _input == Vector2.zero) return;
            
            if (_input.y != 0)
            {
                var target = _input.y > 0 ? _attachedRope.UpPoint.position : _attachedRope.DownPoint.position;
                _rigidbody.transform.position = Vector3.MoveTowards(_rigidbody.transform.position, target, climbSpeed * Time.deltaTime);
            }
        }
    }
}