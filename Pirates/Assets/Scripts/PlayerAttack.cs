using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] swords;
    private Animator anim;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovement.canAttack())
            Attack();

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        anim.SetTrigger("rangeAttack");
        cooldownTimer = 0;

        swords[FindSword()].transform.position = firePoint.position;
        swords[FindSword()].GetComponent<ProjectTile>().SetDirection(Mathf.Sign(transform.localScale.x));
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

}
