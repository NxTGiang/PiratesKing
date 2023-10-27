using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eneny_Sideways : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
