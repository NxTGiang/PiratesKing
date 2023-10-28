using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    private bool invulnerable;
    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if(invulnerable) { return; }
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);       
        if (currentHealth > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invunarablility());
        }
        else
        {
            if (!dead)
            {
                animator.SetBool("grounded", true);
                animator.SetTrigger("die");
                if(GetComponent<PlayerMovement>() != null)
                    GetComponent<PlayerMovement>().enabled = false;

                if(GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if(GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;
                dead = true;
            }
            
        }
    }
    
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunarablility()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for(int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes));
            spriteRenderer.color = Color.white;
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
}
