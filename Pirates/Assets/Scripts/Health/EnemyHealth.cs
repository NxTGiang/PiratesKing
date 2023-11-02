using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    [SerializeField] private float knockbackDistance;
    [SerializeField] private float stuntime;
    [SerializeField] private GameObject floatingTextPrefab;
    private List<Behaviour> components = new List<Behaviour>();
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;
    [SerializeField] FloatingHealthBar healthBar;

    private bool invulnerable;

    public LootBag lootBag;

    private void Awake()
    {
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lootBag = GetComponent<LootBag>();
        healthBar = GetComponentInChildren<FloatingHealthBar>();
    }
    public float getStartingHealth()
    {
        return startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        if (invulnerable) { return; }
        ShowDamage(_damage.ToString());
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        healthBar.UpdateHealthBar(currentHealth, startingHealth);
        Debug.Log(dead);
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
                lootBag.InstantiateLoot(transform.position);
                StartCoroutine(DeactivateAfterDelay(0.5f));
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
    public void setAlive()
    {
        dead = false;
    }
    public bool isDead()
    {
        return dead;
    }
    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    void ShowDamage(string text)
    {
        if (floatingTextPrefab)
        {
            GameObject prefab = Instantiate(floatingTextPrefab, transform.position, Quaternion.identity);
            prefab.GetComponentInChildren<TextMesh>().text = text;
        }
    }
}
