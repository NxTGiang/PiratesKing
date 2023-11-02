using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] swords;
    private float damage = 2;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    private SpriteRenderer spriteRenderer;

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

        if (Input.GetMouseButtonDown(0) && cooldownTimer > attackCooldown /*&& playerMovement.canAttack()*/)
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("rangeAttack");
        cooldownTimer = 0;
        
        swords[FindSword()].transform.position = firePoint.position;
        swords[FindSword()].GetComponent<ProjectTile>().SetDirection(Mathf.Sign(transform.localScale.x), damage);
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

    public void addDamage(float duration, float addDamage)
    {
        damage += addDamage;
        duration -= Time.deltaTime;
        if (duration <= 0)
        {
            damage -= addDamage;
        }
    }

}
