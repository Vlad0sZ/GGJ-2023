using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Main
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Transform characterTransform;
        [SerializeField] private float interactDistance;
        [SerializeField] private Player player;
        [SerializeField] private Swinging swinging;

        private InputActions _inputActions;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private Vector2 _inputDirection;
        private bool _isJump;
        private bool _isSwing;
        private IRope _lastRope;

        private void Awake()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
            _inputActions.Player.Movement.performed += OnMovement;
            _inputActions.Player.Movement.canceled += OnMovementCanceled;
            _inputActions.Player.Jump.performed += OnJumpPerformed;
            _inputActions.Player.Jump.canceled += OnJumpCanceled;
            _inputActions.Player.Interact.performed += OnInteractPerformed;
            
            player.OnLandEvent.AddListener(OnGrounded);
        }

        private void OnGrounded()
        {
            _lastRope = null;
        }

        private void OnJumpCanceled(InputAction.CallbackContext obj)
        {
            _isJump = false;
        }

        private void OnInteractPerformed(InputAction.CallbackContext obj)
        {
            var checkColliders = Physics2D.OverlapCircleAll(characterTransform.position, interactDistance);
            foreach (var checkCollider in checkColliders)
            {
                if(checkCollider.TryGetComponent(out IInteractable interactable))
                    interactable.Interact();
            }
        }

        private void OnJumpPerformed(InputAction.CallbackContext obj)
        {
            Jump();
        }

        private void OnMovementCanceled(InputAction.CallbackContext obj)
        {
            swinging.StopClimb();
            Stop();
        }

        private void Update()
        {
            player.Move(_inputDirection.x, false, _isJump);
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            
            
            if(_isSwing)
                swinging.Climb(value);
            else
                MoveToDirection(value);
            
        }
    
        private void MoveToDirection(Vector2 direction)
        {
            animator.SetBool(IsMoving, true);

            _inputDirection = direction;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out IRope rope))
            {
                if(rope == _lastRope)
                    return;
                
                swinging.Attach(rope);
                Stop();
                _isSwing = true;
                _lastRope = rope;
            }
        }

        private void Stop()
        {
            animator.SetBool(IsMoving, false);
            _inputDirection.x = 0;
        }

        private void Jump()
        {
            _isJump = true;

            if (_isSwing)
            {
                swinging.Detach();
                _isSwing = false;
            }
        }
    }
}
