using UnityEngine;
using UnityEngine.InputSystem;

namespace Main
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private float speed;
        [SerializeField] private Transform characterTransform;
        [SerializeField] private float rayDistance;
        [SerializeField] private float interactDistance;
        [SerializeField] public float jumpAmount = 35;
        [SerializeField] public float gravityScale = 10;
        [SerializeField] public float fallingGravityScale = 40;
    
        private InputActions _inputActions;
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private Rigidbody2D _rigidbody;
        private float _inputDirection;
        private bool _isGround;
        private bool _isJump;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        
            _inputActions = new InputActions();
            _inputActions.Enable();
            _inputActions.Player.Movement.performed += OnMovement;
            _inputActions.Player.Movement.canceled += OnMovementCanceled;
            _inputActions.Player.Jump.performed += OnJumpPerformed;
            _inputActions.Player.Interact.performed += OnInteractPerformed;
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
            Stop();
        }

        private void FixedUpdate()
        {
            var hit = Physics2D.Raycast(_rigidbody.position, Vector2.down, rayDistance,
                LayerMask.GetMask("Ground"));
            _isGround = hit.collider != null;
            
            if (_isJump)
            {
                _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpAmount);
                _isJump = false;
            }

            _rigidbody.gravityScale = _rigidbody.velocity.y switch
            {
                >= 0 => gravityScale,
                < 0 => fallingGravityScale,
                _ => _rigidbody.gravityScale
            };

            _rigidbody.velocity = new Vector2(_inputDirection * speed * Time.fixedDeltaTime, _rigidbody.velocity.y);
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<float>();
            MoveToDirection(value);
        }
    
        private void MoveToDirection(float direction)
        {
            animator.SetBool(IsMoving, true);
            
            var scale = characterTransform.localScale;
            scale.x = direction < 0 ? -1 : 1;
            characterTransform.localScale = scale;

            _inputDirection = direction;
        }
    
        private void Stop()
        {
            animator.SetBool(IsMoving, false);
            _inputDirection = 0;
        }

        private void Jump()
        {
            if(_isGround)
                _isJump = true;
        }
    }
}
