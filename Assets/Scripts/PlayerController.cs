using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private InputAction _moveAction;
    private Vector2 _moveInput;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _jumpHeigh = 3f;
    [SerializeField] private float _playerVelocity = 3f;
    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private Vector2 _sensorSize = new Vector2(0.5f, 0.5f);
    private Animator _animator;




    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _attackAction = InputSystem.actions["Attack"];
        _animator = GetComponent<Animator>();

    }

    void Start()
    {

    }



    void Update()
    {
        //if (groundSensor = Collider.OnTriggerEnter)

        //transform.position = transform.position + new Vector3(_moveInput.x, 0, 0) * _playerVelocity * Time.deltaTime;
        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }
        Movement();
        

    }
    void Movement()
    {
        if (_moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            _animator.SetBool("IsRuning", true);
        }
        else if (_moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            _animator.SetBool("IsRuning", true);
        }
        else
        {
            _animator.SetBool("IsRuning", false);
        }
    }

    void FixedUpdate()
    {

        _moveInput = _moveAction.ReadValue<Vector2>();
        Move();

    }
    void Move()
    {

        _rigidBody.linearVelocity = new Vector2(_moveInput.x * _playerVelocity, _rigidBody.linearVelocityY);
    }

    void Jump()
    {
        _rigidBody.AddForce(transform.up * Mathf.Sqrt(_jumpHeigh * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);

    }

    bool IsGrounded()
    {
        Collider2D[] ground = Physics2D.OverlapBoxAll(_sensorPosition.position, _sensorSize, 0);
        foreach (Collider2D grounds in ground)
        {
            if (grounds.gameObject.layer == 3)
            {
                return true;
            }
        }

        return false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_sensorPosition.position, _sensorSize);
    }




}
