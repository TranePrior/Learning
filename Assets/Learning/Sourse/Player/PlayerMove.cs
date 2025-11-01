using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public sealed class PlayerMove : MonoBehaviour
{
    [SerializeField] float _moveSpeed = 6f;
    [SerializeField] float _jumpForce = 5f;
    [SerializeField] float _jumpCooldown = 1f;

    public event System.Action Jumped;
    
    Rigidbody _rb;
    InputAction _moveAction;
    InputAction _jumpAction;
    Vector2 _move;
    bool _jump;
    bool _canJump = true;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;

        SetupInput();
    }

    void OnEnable()
    {
        _moveAction?.Enable();
        _jumpAction?.Enable();
    }

    void OnDisable()
    {
        _moveAction?.Disable();
        _jumpAction?.Disable();
    }

    void SetupInput()
    {
        _moveAction = new InputAction("Move", InputActionType.Value);
        var kb = _moveAction.AddCompositeBinding("2DVector");
        kb.With("Up", "<Keyboard>/w").With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a").With("Right", "<Keyboard>/d");
        _moveAction.AddBinding("<Gamepad>/leftStick");

        _jumpAction = new InputAction("Jump", InputActionType.Button);
        _jumpAction.AddBinding("<Keyboard>/space");
        _jumpAction.AddBinding("<Gamepad>/buttonSouth");
    }

    void Update()
    {
        _move = _moveAction.ReadValue<Vector2>();
        if (_jumpAction.triggered)
            _jump = true;
    }

    void FixedUpdate()
    {
        Move();
        Jump();
    }

    void Move()
    {
        var direction = new Vector3(_move.x, 0f, _move.y);
        if (direction.sqrMagnitude > 1f) direction.Normalize();

        var displacement = direction * _moveSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + displacement);
    }

    void Jump()
    {
        if (!_jump || !_canJump) { _jump = false; return; }

        _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        
        Jumped?.Invoke();
        _jump = false;
        _canJump = false;
        StartCoroutine(JumpCooldownRoutine());
    }

    IEnumerator JumpCooldownRoutine()
    {
        yield return new WaitForSeconds(_jumpCooldown);
        _canJump = true;
    }
}
