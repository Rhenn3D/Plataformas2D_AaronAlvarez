using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidBody;
    private InputAction _moveAction;
    private Vector2 _moveInput;
    private InputAction _jumpAction;
    private InputAction _interactAction;
    private InputAction _attackAction;
    [SerializeField] private Transform _attackHitbox;
    [SerializeField] private float _attackZone = 0.25f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _playerVelocity = 3f;
    [SerializeField] private float _attackDash = 0.5f;
    [SerializeField] private float _attackDashTime = 0.5f;
    [SerializeField] private Transform _sensorPosition;
    [SerializeField] private Vector2 _sensorSize = new Vector2(0.5f, 0.5f);
    [SerializeField] private Vector2 _hitboxSize = new Vector2(1, 1);
    private Animator _animator;
    private bool _alreadyLanded = true;
    [SerializeField] private float vidaMax = 5f;
    [SerializeField] private float _currentHealth;
    [SerializeField] private bool isAttacking = false;





    void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _moveAction = InputSystem.actions["Move"];
        _jumpAction = InputSystem.actions["Jump"];
        _attackAction = InputSystem.actions["Attack"];
        _interactAction = InputSystem.actions["Interact"];
        _animator = GetComponent<Animator>();

    }

    void Start()
    {
        _currentHealth = vidaMax;
    }



    void Update()
    {
        if (isAttacking)
        {
            return;
        }
        _moveInput = _moveAction.ReadValue<Vector2>();
        
    


        //if (groundSensor = Collider.OnTriggerEnter)


        //transform.position = transform.position + new Vector3(_moveInput.x, 0, 0) * _playerVelocity * Time.deltaTime;
        if (_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }
        Movement();

        if (_interactAction.WasPressedThisFrame())
        {
            Interact();
        }
        _animator.SetBool("IsJumping", !IsGrounded());
        
        if (_attackAction.WasPressedThisFrame() && _moveInput.x == 0 && IsGrounded())
        {
            isAttacking = true;
            _animator.SetTrigger("IsAttacking");
        }

        if (_attackAction.WasPressedThisFrame() && _moveInput.x != 0 && IsGrounded())
        {
            _animator.SetTrigger("IsRunAttack");
        }


       



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
        Move();

    }
    void Move()
    {

        _rigidBody.linearVelocity = new Vector2(_moveInput.x * _playerVelocity, _rigidBody.linearVelocityY);
    }

    void Jump()
    {
        _rigidBody.AddForce(transform.up * Mathf.Sqrt(_jumpForce * -2 * Physics2D.gravity.y), ForceMode2D.Impulse);


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

    void Interact()
    {
        //Debug.Log("hago cositas, miau");
        Collider2D[] interactuables = Physics2D.OverlapBoxAll(transform.position, _hitboxSize, 0);
        foreach (Collider2D item in interactuables)
        {
            if (item.gameObject.layer == 10)
            {
                IInteractable interactables = item.gameObject.GetComponent<IInteractable>();
                if (interactables != null)

                {
                    interactables.Interact();
                }


            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(_sensorPosition.position, _sensorSize);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, _hitboxSize);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(_attackHitbox.position, _attackZone);
    }

    public void NormalAttack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(_attackHitbox.position, _attackZone, 0);
        foreach (Collider2D enemies in enemy)
        {
            if (enemies.gameObject.layer == 8)
            {

                Debug.Log("Mas pegao");
            }
        

        }
        
    }

    public void FinishAttack()
    {
        isAttacking = false;
    }


    public void RecibirDaño(int Dañito)
    {
        _currentHealth -= Dañito;

        float vida = _currentHealth / vidaMax;
        Debug.Log("Holi");

        GUIManager.Instance.UpdateHealthBar(_currentHealth, vidaMax);
        if (vidaMax <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        Debug.Log("Muere");
    }
}
