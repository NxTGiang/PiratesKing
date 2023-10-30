using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpoint;
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void RespawnX()
    {
        playerHealth.Respawn();
        transform.position = currentCheckpoint.position;
        playerHealth.setAlive();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            Console.WriteLine("abv");
           
        }
    }
}
