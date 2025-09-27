using System.Data.Common;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [SerializeField] private float enemySpeed = 2;
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _boxColider;
    [SerializeField] private Vector2 _hitboxSize = new Vector2(1, 1);
    [SerializeField] private int mimikDamage = 1;
    [SerializeField] private float EnemyCooldown = 2f;
    [SerializeField] private float TimerAttack = 2f;
    public int mimikDirection = 1;
    public Transform enemyGround;
    public Vector2 eGroundDetector = new Vector2(1, 1);


    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _boxColider = GetComponent<BoxCollider2D>();
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



    void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player") && EnemyCooldown >= TimerAttack)
        {
            mimikDirection *= -1;
            PlayerController playerScript = collision.gameObject.GetComponent<PlayerController>();
            playerScript.RecibirDaño(mimikDamage);
            EnemyCooldown = 0;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(enemyGround.position, eGroundDetector);
    }


}
