using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    [SerializeField] private float knockbackDistance;
    [SerializeField] private float stuntime;
    private List<Behaviour> components = new List<Behaviour>();
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
        components.AddRange(GetComponents<PlayerMovement>());
        components.AddRange(GetComponents<PlayerAttack>());
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
            StartCoroutine(StunTimer(stuntime));
        }
        else
        {
            if (!dead)
            {
                animator.SetBool("grounded", true);
                animator.SetTrigger("die");
                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
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
        yield return new WaitForSeconds(iFrameDuration);
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }
    private IEnumerator StunTimer(float duration)
    {
        Vector3 knockbackDirection = -transform.forward;
        transform.position += knockbackDirection * knockbackDistance;
        foreach (Behaviour component in components)
        {
            component.enabled = false;
        }
        yield return new WaitForSeconds(duration);
        foreach (Behaviour component in components)
        {
            component.enabled = true;
        }
    }
    public void Respawn()
    {
        AddHealth(startingHealth);
        animator.ResetTrigger("die");
        animator.Play("Idle");
        StartCoroutine(Invunarablility());
        foreach (Behaviour component in components)
            component.enabled = true;
    }
    public void setAlive()
    {
        dead = false;
    }
}
