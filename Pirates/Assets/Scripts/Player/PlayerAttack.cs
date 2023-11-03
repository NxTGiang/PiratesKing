using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] swords;
    [SerializeField] private float damage;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(1) && cooldownTimer > attackCooldown /*&& playerMovement.canAttack()*/)
            Attack();

        if (Input.GetMouseButtonDown(0))
            MeleeAttack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("rangeAttack");
        cooldownTimer = 0;
        
        swords[FindSword()].transform.position = firePoint.position;
        swords[FindSword()].GetComponent<ProjectTile>().SetDirection(Mathf.Sign(transform.localScale.x), damage*2);
    }

    private int FindSword()
    {
        for (int i = 0; i < swords.Length; i++)
        {
            if (!swords[i].activeInHierarchy)
                return i;
        }
        return 0;
    }


    public void addDamage( float addDamage)
    {
        damage += addDamage;
    }
    public void minusDamage(float minusDamage)
    {
        damage -= minusDamage;
    }


    private void MeleeAttack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        
        Collider2D[] hitEnemy = Physics2D.OverlapCircleAll(firePoint.position, attackRange, enemyLayer);
        
        foreach(Collider2D enemy in hitEnemy)
        {
            enemy.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        if(firePoint == null) return;
        Gizmos.DrawWireSphere(firePoint.position, attackRange);
    }


}
