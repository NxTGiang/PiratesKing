using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StarAttack : MonoBehaviour
{
    [Header("Attack parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private float speed;

    [Header("Collider parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private Rigidbody2D rb;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;


    private Animator anim;

    private Health playerHealth;

    private EnemyPatrol enemyPatrol;

    private Vector3 initScale;
    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
        initScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInsight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //cooldownTimer = 0;
                //if (transform.position.x < playerHealth.transform.position.x)
                //    AttackRight();
                //else
                //    AttackLeft();
                anim.SetTrigger("attack");
            }
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInsight();
        }
    }

    private bool PlayerInsight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center - transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
        {
            playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center - transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    public void AttackRight()
    {
        anim.SetTrigger("attack");
        transform.localScale = new Vector3(Mathf.Abs(initScale.x),
            initScale.y, initScale.z);
        Debug.Log("enemy: teleport " + transform.position);
        Debug.Log("player: " + playerHealth.transform.position);
        //transform.parent.position = Vector3.Lerp(transform.position , playerHealth.transform.position, 1);
        //transform.position = Vector2.MoveTowards(transform.position, playerHealth.gameObject.transform.position, speed * Time.deltaTime);
        //rb.MovePosition(Vector2.MoveTowards(transform.position, playerHealth.gameObject.transform.position, speed * Time.deltaTime));
        //transform.position = new Vector3(transform.position.x + Time.deltaTime * _direction * speed,
        //transform.position.y, transform.position.z);

        //transform.Translate(speed * Time.fixedDeltaTime * 1 * 100, 0, 0);

        rb.AddForce(Vector2.left * 40, ForceMode2D.Impulse);
        /* rb.velocity = new Vector2(speed * 1.2f, 0);
         transform.localScale = new Vector2(1, 0);*/

       
    }

    public void AttackLeft()
    {
        anim.SetTrigger("attack");
        transform.localScale = new Vector3(Mathf.Abs(initScale.x) * -1,
            initScale.y, initScale.z);
        Debug.Log("enemy: teleport " + transform.position);
        Debug.Log("player: " + playerHealth.transform.position);
        rb.velocity = new Vector2(-speed*1.2f, 0);
        transform.localScale = new Vector2(-1, 0);

    }

}
