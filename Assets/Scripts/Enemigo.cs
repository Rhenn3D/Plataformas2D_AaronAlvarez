using System.Data.Common;
using UnityEngine;
using System.Collections;

public class Enemigo : MonoBehaviour, IDamageable
{
    [SerializeField] private float enemySpeed = 2;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxColider;
    [SerializeField] private Vector2 _hitboxSize = new Vector2(1, 1);
    [SerializeField] private int mimikDamage = 1;
    [SerializeField] private float EnemyCooldown = 2f;
    [SerializeField] private float TimerAttack = 2f;
    [SerializeField] private int enemyHealth = 9;
    public int mimikDirection = 1;
    public Transform enemyGround;
    public Vector2 eGroundDetector = new Vector2(1, 1);
    private Animator _animator;

    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider;
    private AudioSource _audioSource;
    public AudioClip _deadEnemySFX;



    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _rigidbody.linearVelocity = new Vector2(enemySpeed * mimikDirection, _rigidbody.linearVelocity.y);
        EnemyCooldown += 1 * Time.deltaTime;
        if (EnemyCooldown == TimerAttack)
        {
            Debug.Log("Ya toy");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            mimikDirection *= -1;
        }
    }

    



    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            mimikDirection *= -1;
            if (collision.gameObject.CompareTag("Player") && EnemyCooldown >= TimerAttack)
            {
                PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
                playerScript.RecibirDaño(mimikDamage);
                EnemyCooldown = 0;
                _animator.SetTrigger("IsAttacking");
            }
        }
    }


    public void DamageEnemy(int damage)
    {
        enemyHealth -= damage;
        Debug.Log("Enemy Health: " + enemyHealth);
        if (enemyHealth <= 0)
        {
            StartCoroutine(DeadEnemy());
        }
    }

    IEnumerator DeadEnemy()
    {
        _audioSource.PlayOneShot(_deadEnemySFX);
        _spriteRenderer.enabled = false;
        _rigidbody.gravityScale = 0;
        _boxCollider.enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(enemyGround.position, eGroundDetector);
    }


}
